namespace Agile.Ksp.UI
{
    public abstract class KspUIControl
    {
        public KspUIContainer Parent { get; internal set; }

        public abstract void Draw();
    }
}