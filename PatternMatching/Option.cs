using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternMatching
{
    public struct Option<T>
    {
        private readonly bool isSome;
        public bool IsSome
        {
            get
            {
                return isSome;
            }
        }

        private readonly T value;
        public T Value
        {
            get
            {
                if (!isSome) throw new NotSupportedException();
                return value;
            }
        }

        private Option(bool isSome, T value)
        {
            this.isSome = isSome;
            this.value = value;
        }

        public static Option<T> Some(T value)
        {
            return new Option<T>(true, value);
        }

        public static Option<T> None()
        {
            return new Option<T>(false, default(T));
        }

        public override int GetHashCode()
        {
            return isSome
                ? value.GetHashCode()
                : -1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Option<T>))
                return false;

            Option<T> otherOption = (Option<T>)other;

            return IsSome
                ? (value == null && otherOption.Value == null)
                    || value.Equals(otherOption.Value)
                : !otherOption.IsSome;
        }

        public override string ToString()
        {
            return this.IsSome
                ? this.value.ToString()
                : String.Empty;
        }
    }
}
