using System;

namespace PatternMatching
{
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException()
            : base("The input hasn't matched any of the cases")
        {
        }
    }
}
