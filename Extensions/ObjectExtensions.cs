using System;

namespace Agile.Ksp.Extensions
{
    public static class ObjectExtensions
    {
        public static T Tap<T>(this T o, Action<T> action)
        {
            action(o);
            return o;
        }
    }
}