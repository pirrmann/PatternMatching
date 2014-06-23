using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternMatching
{
    public class MatchCase
    {
        public static readonly MatchCase Any;

        static MatchCase()
        {
            Any = new MatchCase();
        }

        private MatchCase()
        {
        }
    }

    internal class MatchCase<TSource, TResult>
    {
        private readonly Expression<Func<TSource, bool>> testExpression;
        private readonly Expression<Func<TSource, TResult>> selector;

        public MatchCase(
            Expression<Func<TSource, bool>> testExpression,
            Expression<Func<TSource, TResult>> selector)
        {
            this.testExpression = testExpression;
            this.selector = selector;
        }

        public Expression<Func<TSource, bool>> TestExpression
        {
            get { return this.testExpression; }
        }

        public Expression<Func<TSource, TResult>> SelectorExpression
        {
            get { return this.selector; }
        }
    }
}
