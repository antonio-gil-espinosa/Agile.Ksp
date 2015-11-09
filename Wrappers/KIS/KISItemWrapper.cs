namespace Agile.Ksp.Wrappers.KIS
{
    public class KISItemWrapper
    {
        private KISItemWrapper(object item)
        {
        }

        public static KISItemWrapper FromObject(object invoke)
        {
            if (invoke != null)
                return new KISItemWrapper(invoke);

            return null;
        }
    }
}