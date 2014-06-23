using PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class BlogSamples
    {
        PatternMatcher<string, double> stringPattern = null;

        public void CreatePatterns()
        {
            var _ = MatchCase.Any;

            this.stringPattern = PatternMatcher
                .CreateSwitch<string, double>()
                .Case("TEST", s => Math.PI)
                .Case("TEST_1", s => Math.E)
                .Case((string)null, s => 0.0)
                .Case(s => s.StartsWith("TEST__"), s => Math.E * 2)
                .CaseElse(s => s.Length);

            Func<string, double> generatedFunc =
                (string s) =>
                {
                    if (s == "TEST") return Math.PI;
                    if (s == "TEST_1") return Math.E;
                    if (s == null) return 0.0;
                    if (s.StartsWith("TEST__")) return Math.E * 2;
                    return s.Length;
                };

            Func<string, double> generatedFuncNoIf =
                (string s) =>
                    (s == "TEST")
                        ? Math.PI
                        : (s == "TEST_1")
                            ? Math.E
                            : (s == null)
                                ? 0.0
                                : s.StartsWith("TEST__")
                                    ? Math.E * 2
                                    : s.Length;

            Func<string, Option<double>> generatedFuncOption =
                (string s) =>
                    (s == "TEST")
                        ? Option<double>.Some(Math.PI)
                        : (s == "TEST_1")
                            ? Option<double>.Some(Math.E)
                            : (s == null)
                                ? Option<double>.Some(0.0)
                                : s.StartsWith("TEST__")
                                    ? Option<double>.Some(Math.E * 2)
                                    : Option<double>.Some(s.Length);

            Func<double, Option<double>> matchSomeOrNone =
                (double d) =>
                    (d > 0.0)
                    ? Option<double>.Some(d)
                    : Option<double>.None();

            var tupleSample1 =
                PatternMatcher.CreateSwitch<int, int, int>()
                .Case(1, 1, (a, b) => 1)
                .Case(2, _, (a, b) => 2)
                .Case(_, 3, (a, b) => 3)
                .Case(a => a > 4, _, (a, b) => 4)
                .Case(_, _, (a, b) => 5);
        }
    }
}