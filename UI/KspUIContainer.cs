using System.Collections.Generic;

namespace Agile.Ksp.UI
{
    public abstract class KspUIContainer : KspUIControl
    {
        public override void Draw()
        {
            foreach (KspUIControl control in Controls)
            {
                control.Draw();
            }
        }

        public IList<KspUIControl> Controls { get; private set; } = new List<KspUIControl>();

        public void AddControl(KspUIControl control)
        {
            Controls.Add(control);
            control.Parent = this;
        }
    }
}