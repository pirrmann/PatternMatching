using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternMatching;

namespace Tests
{
    [TestClass]
    public class PatternMatchingTests
    {
        PatternMatcher<string, double> stringPattern = null;
        PatternMatcher<string, double> stringPatternWithNoCase = null;
        PatternMatcher<Tuple<int, string>, double> tuple2Pattern = null;
        PatternMatcher<Tuple<int, string, double>, int> tuple3Pattern = null;

        [TestInitialize]
        public void CreatePatterns()
        {
            var _ = MatchCase.Any;

            this.stringPattern = PatternMatcher
                .CreateSwitch<string, double>()
                .Case("TEST"                        , s => Math.PI)
                .Case("TEST_1"                      , s => Math.E)
                .Case((string)null                  , s => 0.0)
                .Case(s => s.StartsWith("TEST__")   , s => Math.E * 2)
                .CaseElse(                            s => s.Length);

            this.stringPatternWithNoCase = PatternMatcher
                .CreateSwitch<string, double>();

            this.tuple2Pattern = PatternMatcher
                .CreateSwitch<int, string, double>()

                .Case(1, (string)null,
                        (x, y) => x * 2)
                .Case(1, s => s.Length < 5,
                        (x, y) => x + y.Length)
                .Case(2, _,
                        (x, y) => Math.PI)
                .Case(i => i % 100 == 0, "modulo",
                        (x, y) => x / 100)
                .Case(i => i % 10 == 0, s => s != null && s.StartsWith("divide"),
                        (x, y) => x / 1000.0)
                .Case(i => i % 10 == 0, _,
                        (x, y) => x / 10000.0)
                .Case(_, "E",
                        (x, y) => x * Math.E)
                .Case(t => t.Item2 != null && t.Item1 == t.Item2.Length,
                        t => t.Item1)
                .Case(_, s => s != null,
                        (x, y) => x * Math.E * -1)
                .CaseElse(
                        t => 1.0);

            this.tuple3Pattern = PatternMatcher
                .CreateSwitch<int, string, double, int>()
                .Case(0, "false", 0.0,
                        (i, s, d) => 1)
                .Case(0, "false", d => d > 0.0,
                        (i, s, d) => 2)
                .Case(0, "false", _,
                        (i, s, d) => 3)
                .Case(0, s => s.Length > 10, 0.0,
                        (i, s, d) => 4)
                .Case(0, s => s.Length > 10, d => d > 0.0,
                        (i, s, d) => 5)
                .Case(0, s => s.Length > 10, _,
                        (i, s, d) => 6)
                .Case(0, _, 0.0,
                        (i, s, d) => 7)
                .Case(0, _, d => d > 0.0,
                        (i, s, d) => 8)
                .Case(0, _, _,
                        (i, s, d) => 9)

                .Case(i => i > 10, "false", 0.0,
                        (i, s, d) => 10)
                .Case(i => i > 10, "false", d => d > 0.0,
                        (i, s, d) => 11)
                .Case(i => i > 10, "false", _,
                        (i, s, d) => 12)
                .Case(i => i > 10, s => s.Length > 10, 0.0,
                        (i, s, d) => 13)
                .Case(i => i > 10, s => s.Length > 10, d => d > 0.0,
                        (i, s, d) => 14)
                .Case(i => i > 10, s => s.Length > 10, _,
                        (i, s, d) => 15)
                .Case(i => i > 10, _, 0.0,
                        (i, s, d) => 16)
                .Case(i => i > 10, _, d => d > 0.0,
                        (i, s, d) => 17)
                .Case(i => i > 10, _, _,
                        (i, s, d) => 18)

                .Case(_, "false", 0.0,
                        (i, s, d) => 19)
                .Case(_, "false", d => d > 0.0,
                        (i, s, d) => 20)
                .Case(_, "false", _,
                        (i, s, d) => 21)
                .Case(_, s => s.Length > 10, 0.0,
                        (i, s, d) => 22)
                .Case(_, s => s.Length > 10, d => d > 0.0,
                        (i, s, d) => 23)
                .Case(_, s => s.Length > 10, _,
                        (i, s, d) => 24)
                .Case(_, _, 0.0,
                        (i, s, d) => 25)
                .Case(_, _, d => d > 0.0,
                        (i, s, d) => 26)

                .CaseElse(
                        t => 27);
        }

        #region Simple

        [TestMethod]
        public void SimpleValue()
        {
            string candidate = "TEST_1";

            var test = this.stringPattern.Eval(candidate);

            Assert.AreEqual(Math.E, test);
        }

        [TestMethod]
        public void SimpleExpresion()
        {
            string candidate = "TEST__3";

            var test = this.stringPattern.Eval(candidate);

            Assert.AreEqual(2 * Math.E, test);
        }

        [TestMethod]
        public void NullExpresion()
        {
            string candidate = null;

            var test = this.stringPattern.Eval(candidate);

            Assert.AreEqual(0.0, test);
        }

        [TestMethod]
        public void NotMatching()
        {
            Exception thrownException = null;
            try
            {
                string candidate = "TEST_2";

                var test = this.stringPatternWithNoCase.Eval(candidate);
            }
            catch (Exception e)
            {
                thrownException = e;
            }

            Assert.IsInstanceOfType(thrownException, typeof(NoMatchFoundException));
        }

        [TestMethod]
        public void NotMatchingWithAny()
        {
            string candidate = "I'm not gonna match !";

            var test = this.stringPattern.Eval(candidate);

            Assert.AreEqual(21.0, test);
        }

        #endregion

        #region 2-uples

        [TestMethod]
        public void Tuple2ByValues()
        {
            var candidate = Tuple.Create(1, (string)null);

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(2.0, test);
        }

        [TestMethod]
        public void Tuple2ByValueExpression()
        {
            var candidate = Tuple.Create(1, "one");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(4.0, test);
        }

        [TestMethod]
        public void Tuple2ByValueAny()
        {
            var candidate = Tuple.Create(2, "We don't really care...");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(Math.PI, test);
        }

        [TestMethod]
        public void Tuple2ByExpressionValue()
        {
            var candidate = Tuple.Create(31400, "modulo");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(314.0, test);
        }

        [TestMethod]
        public void Tuple2ByExpressionExpression()
        {
            var candidate = Tuple.Create(3140, "divideagain");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(3.14, test);
        }

        [TestMethod]
        public void Tuple2ByExpressionAny()
        {
            var candidate = Tuple.Create(3140, string.Empty);

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(0.314, test);
        }

        [TestMethod]
        public void Tuple2ByAnyValue()
        {
            var candidate = Tuple.Create(5, "E");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(Math.E * 5, test);
        }

        [TestMethod]
        public void Tuple2ByAnyExpression()
        {
            var candidate = Tuple.Create(5, "Not E at all");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(Math.E * -5, test);
        }

        [TestMethod]
        public void Tuple2ByExpression()
        {
            var candidate = Tuple.Create(26, "AbCdEfGhIjKlMnOpQrStUvWxYz");

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(26.0, test);
        }

        [TestMethod]
        public void Tuple2ByAny()
        {
            var candidate = Tuple.Create(-1, (string)null);

            var test = this.tuple2Pattern.Eval(candidate);

            Assert.AreEqual(1.0, test);
        }

        #endregion

        #region 3-uples

        [TestMethod]
        public void Tuple3_1_ByValueValueValue()
        {
            var candidate = Tuple.Create(0, "false", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void Tuple3_2_ByValueValueExpression()
        {
            var candidate = Tuple.Create(0, "false", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(2, test);
        }

        [TestMethod]
        public void Tuple3_3_ByValueValueAny()
        {
            var candidate = Tuple.Create(0, "false", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(3, test);
        }

        [TestMethod]
        public void Tuple3_4_ByValueExpressionValue()
        {
            var candidate = Tuple.Create(0, "more than 10 chars", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(4, test);
        }

        [TestMethod]
        public void Tuple3_5_ByValueExpressionExpression()
        {
            var candidate = Tuple.Create(0, "more than 10 chars", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(5, test);
        }

        [TestMethod]
        public void Tuple3_6_ByValueExpressionAny()
        {
            var candidate = Tuple.Create(0, "more than 10 chars", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(6, test);
        }

        [TestMethod]
        public void Tuple3_7_ByValueAnyValue()
        {
            var candidate = Tuple.Create(0, string.Empty, 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(7, test);
        }

        [TestMethod]
        public void Tuple3_8_ByValueAnyExpression()
        {
            var candidate = Tuple.Create(0, string.Empty, 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(8, test);
        }

        [TestMethod]
        public void Tuple3_9_ByValueAnyAny()
        {
            var candidate = Tuple.Create(0, string.Empty, - 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(9, test);
        }

        [TestMethod]
        public void Tuple3_10_ByExpressionValueValue()
        {
            var candidate = Tuple.Create(100, "false", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(10, test);
        }

        [TestMethod]
        public void Tuple3_11_ByExpressionValueExpression()
        {
            var candidate = Tuple.Create(100, "false", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(11, test);
        }

        [TestMethod]
        public void Tuple3_12_ByExpressionValueAny()
        {
            var candidate = Tuple.Create(100, "false", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(12, test);
        }

        [TestMethod]
        public void Tuple3_13_ByExpressionExpressionValue()
        {
            var candidate = Tuple.Create(100, "more than 10 chars", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(13, test);
        }

        [TestMethod]
        public void Tuple3_14_ByExpressionExpressionExpression()
        {
            var candidate = Tuple.Create(100, "more than 10 chars", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(14, test);
        }

        [TestMethod]
        public void Tuple3_15_ByExpressionExpressionAny()
        {
            var candidate = Tuple.Create(100, "more than 10 chars", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(15, test);
        }

        [TestMethod]
        public void Tuple3_16_ByExpressionAnyValue()
        {
            var candidate = Tuple.Create(100, string.Empty, 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(16, test);
        }

        [TestMethod]
        public void Tuple3_17_ByExpressionAnyExpression()
        {
            var candidate = Tuple.Create(100, string.Empty, 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(17, test);
        }

        [TestMethod]
        public void Tuple3_18_ByExpressionAnyAny()
        {
            var candidate = Tuple.Create(100, string.Empty, -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(18, test);
        }

        [TestMethod]
        public void Tuple3_19_ByAnyValueValue()
        {
            var candidate = Tuple.Create(-100, "false", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(19, test);
        }

        [TestMethod]
        public void Tuple3_20_ByAnyValueExpression()
        {
            var candidate = Tuple.Create(-100, "false", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(20, test);
        }

        [TestMethod]
        public void Tuple3_21_ByAnyValueAny()
        {
            var candidate = Tuple.Create(-100, "false", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(21, test);
        }

        [TestMethod]
        public void Tuple3_22_ByAnyExpressionValue()
        {
            var candidate = Tuple.Create(-100, "more than 10 chars", 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(22, test);
        }

        [TestMethod]
        public void Tuple3_23_ByAnyExpressionExpression()
        {
            var candidate = Tuple.Create(-100, "more than 10 chars", 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(23, test);
        }

        [TestMethod]
        public void Tuple3_24_ByAnyExpressionAny()
        {
            var candidate = Tuple.Create(-100, "more than 10 chars", -1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(24, test);
        }

        [TestMethod]
        public void Tuple3_25_ByAnyAnyValue()
        {
            var candidate = Tuple.Create(-100, string.Empty, 0.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(25, test);
        }

        [TestMethod]
        public void Tuple3_26_ByAnyAnyExpression()
        {
            var candidate = Tuple.Create(-100, string.Empty, 1.0);

            var test = this.tuple3Pattern.Eval(candidate);

            Assert.AreEqual(26, test);
        }

        #endregion
    }
}
