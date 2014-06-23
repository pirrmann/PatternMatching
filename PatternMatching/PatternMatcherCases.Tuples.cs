using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PatternMatching
{
    public static partial class PatternMatcherCases
    {
		private static Expression CombineAll(this IEnumerable<Expression> expressions)
        {
            Expression aggregateExpression =
                expressions.Aggregate(
                    (Expression)null,
                    (a, e) => a == null ? e : Expression.AndAlso(a, e));

            return aggregateExpression ?? Expression.Constant(true);
        }

        #region 2-uple
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			T1 testValue1,
			T2 testValue2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			T1 testValue1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			T1 testValue1,
			MatchCase any2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			T2 testValue2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			MatchCase any2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			MatchCase any1,
			T2 testValue2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			MatchCase any1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2>, TResult> Case<T1, T2, TResult>(
			this PatternMatcher<Tuple<T1, T2>, TResult> pattern,
			MatchCase any1,
			MatchCase any2,
			Expression<Func<T1, T2, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2>();

			List<Expression> testExpressions = new List<Expression>();

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2>, TResult>(
                    testExpression,
                    selectorExpression));
		}

		#region Factored methods

		private static MemberInfo[] GetTupleMembers<T1, T2>()
		{
			Type tupleType = typeof(Tuple<T1, T2>);

			MemberInfo[] members = new MemberInfo[2];

            members[0] = tupleType.GetProperty("Item1");
            members[1] = tupleType.GetProperty("Item2");

			return members;
		}

		private static Expression<Func<Tuple<T1, T2>, TResult>> GetSelectorExpression<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> selector, MemberInfo[] members)
		{
			ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2>), "t");
            var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2>, TResult>>(
                Expression.Invoke(
                    selector,
					Expression.MakeMemberAccess(selectorParam, members[0]),
					Expression.MakeMemberAccess(selectorParam, members[1])),
                selectorParam);

			return selectorExpression;
		}

		#endregion

		#endregion

        #region 3-uple
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			T2 testValue2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			T2 testValue2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			T2 testValue2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			Expression<Func<T2, bool>> testExpression2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			Expression<Func<T2, bool>> testExpression2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			MatchCase any2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			MatchCase any2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			T1 testValue1,
			MatchCase any2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[0]),
					Expression.Constant(testValue1)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			T2 testValue2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			T2 testValue2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			T2 testValue2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			Expression<Func<T2, bool>> testExpression2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			Expression<Func<T2, bool>> testExpression2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			MatchCase any2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			MatchCase any2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			Expression<Func<T1, bool>> testExpression1,
			MatchCase any2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression1,
                    Expression.MakeMemberAccess(testParam, members[0])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			T2 testValue2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			T2 testValue2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			T2 testValue2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[1]),
					Expression.Constant(testValue2)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			Expression<Func<T2, bool>> testExpression2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			Expression<Func<T2, bool>> testExpression2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			Expression<Func<T2, bool>> testExpression2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression2,
                    Expression.MakeMemberAccess(testParam, members[1])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			MatchCase any2,
			T3 testValue3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
				Expression.Equal(
					Expression.MakeMemberAccess(testParam, members[2]),
					Expression.Constant(testValue3)));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			MatchCase any2,
			Expression<Func<T3, bool>> testExpression3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();
			testExpressions.Add(
                Expression.Invoke(
                    testExpression3,
                    Expression.MakeMemberAccess(testParam, members[2])));

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}
	
		public static PatternMatcher<Tuple<T1, T2, T3>, TResult> Case<T1, T2, T3, TResult>(
			this PatternMatcher<Tuple<T1, T2, T3>, TResult> pattern,
			MatchCase any1,
			MatchCase any2,
			MatchCase any3,
			Expression<Func<T1, T2, T3, TResult>> selector)
		{
			ParameterExpression testParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
			MemberInfo[] members = GetTupleMembers<T1, T2, T3>();

			List<Expression> testExpressions = new List<Expression>();

			Expression aggregateExpression = testExpressions.CombineAll();

            var testExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, bool>>(
                aggregateExpression,
                testParam);

            var selectorExpression = GetSelectorExpression<T1, T2, T3, TResult>(selector, members);

            return new PatternMatcher<Tuple<T1, T2, T3>, TResult>(
                pattern,
                new MatchCase<Tuple<T1, T2, T3>, TResult>(
                    testExpression,
                    selectorExpression));
		}

		#region Factored methods

		private static MemberInfo[] GetTupleMembers<T1, T2, T3>()
		{
			Type tupleType = typeof(Tuple<T1, T2, T3>);

			MemberInfo[] members = new MemberInfo[3];

            members[0] = tupleType.GetProperty("Item1");
            members[1] = tupleType.GetProperty("Item2");
            members[2] = tupleType.GetProperty("Item3");

			return members;
		}

		private static Expression<Func<Tuple<T1, T2, T3>, TResult>> GetSelectorExpression<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> selector, MemberInfo[] members)
		{
			ParameterExpression selectorParam = Expression.Parameter(typeof(Tuple<T1, T2, T3>), "t");
            var selectorExpression = Expression.Lambda<Func<Tuple<T1, T2, T3>, TResult>>(
                Expression.Invoke(
                    selector,
					Expression.MakeMemberAccess(selectorParam, members[0]),
					Expression.MakeMemberAccess(selectorParam, members[1]),
					Expression.MakeMemberAccess(selectorParam, members[2])),
                selectorParam);

			return selectorExpression;
		}

		#endregion

		#endregion

    }
}
