using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PatternMatching
{
    public static partial class PatternMatcherCases
    {
        #region no constraint

        public static PatternMatcher<TSource, TResult> CaseElse<TSource, TResult>(
            this PatternMatcher<TSource, TResult> pattern,
            Expression<Func<TSource, TResult>> selector)
        {
            return new PatternMatcher<TSource, TResult>(
                pattern,
                new MatchCase<TSource, TResult>(s => true, selector));
        }

        #endregion

        #region 1-uple

        public static PatternMatcher<TSource, TResult> Case<TSource, TResult>(
            this PatternMatcher<TSource, TResult> pattern, TSource testValue,
            Expression<Func<TSource, TResult>> selector)
        {
            Expression<Func<TSource, bool>> testExpression = s => s == null;

            if (testValue != null)
            {
                var parameterExpr = Expression.Parameter(typeof(TSource), "x");
                testExpression =
                    Expression.Lambda<Func<TSource, bool>>(
                        Expression.Equal(
                            parameterExpr,
                            Expression.Constant(testValue, typeof(TSource))),
                        parameterExpr);
            }
            
            return new PatternMatcher<TSource, TResult>(
                pattern,
                new MatchCase<TSource, TResult>(testExpression, selector));
        }

        public static PatternMatcher<TSource, TResult>
            Case<TSource, TResult>(
            this PatternMatcher<TSource, TResult> pattern,
            Expression<Func<TSource, bool>> testExpression,
            Expression<Func<TSource, TResult>> selector)
        {
            return new PatternMatcher<TSource, TResult>(
                pattern,
                new MatchCase<TSource, TResult>(testExpression, selector));
        }

        public static PatternMatcher<TSource, TResult> Case<TSource, TResult>(
            this PatternMatcher<TSource, TResult> pattern,
            MatchCase any,
            Expression<Func<TSource,TResult>> selector)
        {
            return pattern.CaseElse(selector);
        }

        #endregion

        //#region 2-uple

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, T1 testValue1, T2 testValue2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.AndAlso(
        //                Expression.Equal(
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem1),
        //                    Expression.Constant(testValue1)),
        //                Expression.Equal(
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem2),
        //                    Expression.Constant(testValue2)))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, T1 testValue1, Expression<Func<T2, bool>> testExpression2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.AndAlso(
        //                Expression.Equal(
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem1),
        //                    Expression.Constant(testValue1)),
        //                Expression.Invoke(
        //                    testExpression2,
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem2)))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, T1 testValue1, MatchCase any, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.Equal(
        //                Expression.MakeMemberAccess(testParam, memberInfoItem1),
        //                Expression.Constant(testValue1))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, Expression<Func<T1, bool>> testExpression1, T2 testValue2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.AndAlso(
        //                Expression.Invoke(
        //                    testExpression1,
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem1)),
        //                Expression.Equal(
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem2),
        //                    Expression.Constant(testValue2)))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, Expression<Func<T1, bool>> testExpression1, Expression<Func<T2, bool>> testExpression2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.AndAlso(
        //                Expression.Invoke(
        //                    testExpression1,
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem1)),
        //                Expression.Invoke(
        //                    testExpression2,
        //                    Expression.MakeMemberAccess(testParam, memberInfoItem2)))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, Expression<Func<T1, bool>> testExpression1, MatchCase any, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.Invoke(
        //                testExpression1,
        //                Expression.MakeMemberAccess(testParam, memberInfoItem1))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, MatchCase any, T2 testValue2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.Equal(
        //                Expression.MakeMemberAccess(testParam, memberInfoItem2),
        //                Expression.Constant(testValue2))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(this PatternMatcher<Tuple<T1, T2>, TResult> pattern, MatchCase any, Expression<Func<T2, bool>> testExpression2, Expression<Func<T1, T2, TResult>> selector)
        //{
        //    Type tupleType = typeof(Tuple<T1, T2>);

        //    MemberInfo memberInfoItem1 = tupleType.GetProperty("Item1");
        //    MemberInfo memberInfoItem2 = tupleType.GetProperty("Item2");

        //    ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
        //        Expression.AndAlso(
        //            Expression.NotEqual(testParam, Expression.Constant(null, tupleType)),
        //            Expression.Invoke(
        //                testExpression2,
        //                Expression.MakeMemberAccess(testParam, memberInfoItem2))),
        //        testParam);

        //    ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
        //    var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
        //        Expression.Invoke(
        //            selector,
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem1),
        //            Expression.MakeMemberAccess(selectorParam, memberInfoItem2)),
        //        selectorParam);

        //    return new PatternMatcher<Tuple<T1, T2>, TResult>(
        //        pattern,
        //        new MatchCase<Tuple<T1, T2>, TResult>(
        //            testExpression,
        //            selectorExpression));
        //}

        //#endregion
    }
}
