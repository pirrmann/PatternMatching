using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PatternMatching
{
    public static class PatternMatcher
    {
        public static PatternMatcher<TSource, TResult> CreateSwitch<TSource, TResult>()
        {
            return new PatternMatcher<TSource, TResult>();
        }

        public static PatternMatcher<Tuple<T1, T2>, TResult> CreateSwitch<T1, T2, TResult>()
        {
            return new PatternMatcher<Tuple<T1, T2>, TResult>();
        }

        public static PatternMatcher<Tuple<T1, T2, T3>, TResult> CreateSwitch<T1, T2, T3, TResult>()
        {
            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>();
        }
    }

    public class PatternMatcher<TSource, TResult>
    {
        private static MethodInfo createSomeMethodInfo;
        private static MethodInfo createNoneMethodInfo;

        private readonly IEnumerable<MatchCase<TSource, TResult>> cases;
        private Func<TSource, Option<TResult>> patternMatchingFunc = null;

        static PatternMatcher()
        {
            createSomeMethodInfo = typeof(Option<TResult>).GetMethod("Some", BindingFlags.Public | BindingFlags.Static);
            createNoneMethodInfo = typeof(Option<TResult>).GetMethod("None", BindingFlags.Public | BindingFlags.Static);
        }

        internal PatternMatcher()
        {
            this.cases = Enumerable.Empty<MatchCase<TSource, TResult>>();
        }

        internal PatternMatcher(PatternMatcher<TSource, TResult> basePattern, MatchCase<TSource, TResult> additionalCase)
        {
            this.cases = basePattern.cases.Concat(new[] { additionalCase }).ToArray();
        }

        private Func<TSource, Option<TResult>> BuildPatternMatchingFunc()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource));

            Expression noneExpression = Expression.Call(createNoneMethodInfo);

            Expression caseExpression = noneExpression;

            foreach (var matchCase in this.cases.Reverse())
            {
                caseExpression =
                    Expression.Condition(
                        Expression.Invoke(matchCase.TestExpression, parameter),
                        Expression.Call(
                            createSomeMethodInfo,
                            Expression.Invoke(matchCase.SelectorExpression, parameter)),
                        caseExpression);
            }

            string visu = TreeVisualizer.BuildVisualization(caseExpression, false);

            return Expression
                .Lambda<Func<TSource, Option<TResult>>>(
                    caseExpression,
                    parameter)
                .Compile();
        }

        public TResult Eval(TSource candidate)
        {
            if (this.patternMatchingFunc == null)
            {
                this.patternMatchingFunc = BuildPatternMatchingFunc();
            }

            Option<TResult> matchResult = patternMatchingFunc
                                            .Invoke(candidate);

            if (!matchResult.IsSome)
                throw new NoMatchFoundException();

            return matchResult.Value;
        }
    }
}
