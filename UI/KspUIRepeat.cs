using System;
using System.Collections.Generic;

namespace Agile.Ksp.UI
{
    public class KspUIRepeat<T> : KspUIControl
    {
        private readonly Func<IEnumerable<T>> _getter;
        private readonly Func<T, KspUIControl> _action;

        public KspUIRepeat(Func<IEnumerable<T>> getter, Func<T, KspUIControl> action)
        {
            _getter = getter;
            _action = action;
        }

        public override void Draw()
        {
            foreach (var i in _getter.Invoke())
            {
                _action.Invoke(i).Draw();
            }
        }
    }

    public static class KspUIRepeat
    {
        public static KspUIRepeat<T> Create<T>(Func<IEnumerable<T>> getter, Func<T, KspUIControl> action)
        {
            return new KspUIRepeat<T>(getter, action);
        }
    }
}