using Agile.Ksp.Extensions;
using System;
using UnityEngine;

namespace Agile.Ksp.Wrappers.KIS
{
    public class KISItemModuleWrapper
    {
        private readonly Component component;

        private KISItemModuleWrapper(Component component)
        {
            this.component = component;
        }

        public float VolumeOverride => Convert.ToSingle(component.GetFieldValue("volumeOverride"));

        public static KISItemModuleWrapper FromComponent(Component component)
        {
            if (component != null)
                return new KISItemModuleWrapper(component);

            return null;
        }
    }
}