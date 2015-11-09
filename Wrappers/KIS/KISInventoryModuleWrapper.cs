using Agile.Ksp.Extensions;
using System;

namespace Agile.Ksp.Wrappers.KIS
{
    public class KISInventoryModuleWrapper
    {
        private readonly PartModule _partModule;

        public static KISInventoryModuleWrapper FromComponent(PartModule component)
        {
            if (component != null)
                return new KISInventoryModuleWrapper(component);

            return null;
        }

        private KISInventoryModuleWrapper(PartModule module)
        {
            _partModule = module;
        }

        public float MaxVolume => Convert.ToSingle(_partModule.GetFieldValue("maxVolume"));
        public string Name => Convert.ToString(_partModule.GetFieldValue("invName"));

        public KISItemWrapper AddItem(Part part, float qty = 1, int slot = -1)
        {
            return
                KISItemWrapper.FromObject(_partModule.InvokeMethod("AddItem", new object[] { part, qty, slot },
                                                                   new[]
                                                                   {
                                                                       typeof (Part),
                                                                       typeof (float),
                                                                       typeof (int)
                                                                   }));
        }

        public KISItemWrapper AddItem(AvailablePart aPart, ConfigNode configNode, float quantity = 1, int slot = -1)
        {
            return
                KISItemWrapper.FromObject(_partModule.InvokeMethod("AddItem", new object[] { aPart, configNode, quantity, slot },
                                                                   new[]
                                                                   {
                                                                       typeof (AvailablePart),
                                                                       typeof (ConfigNode),
                                                                       typeof (float),
                                                                       typeof (int)
                                                                   }));
        }

        public void RefreshMassAndVolume()
        {
            _partModule.InvokeMethod("RefreshMassAndVolume", new object[] { }, new Type[] { });
        }
    }
}