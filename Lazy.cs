using System;

namespace Agile.Ksp
{
    public class Lazy
    {
        public static Lazy<T> For<T>(Func<T> getter)
        {
            return new Lazy<T>(getter);
        }
    }

    public class Lazy<T>
    {
        private T _value;

        private readonly Func<T> _getter;

        public Lazy(Func<T> getter)
        {
            _getter = getter;
        }

        public bool Created
        {
            get;
            private set;
        }

        public T Value
        {
            get
            {
                if (Created)
                    return _value;

                _value = _getter();
                Created = true;
                return _value; ;
            }
        }
    }
}