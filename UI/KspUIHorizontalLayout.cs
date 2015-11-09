using UnityEngine;

namespace Agile.Ksp.UI
{
    public class KspUIHorizontalLayout : KspUIContainer
    {
        public override void Draw()
        {
            GUILayout.BeginHorizontal();
            base.Draw();
            GUILayout.EndHorizontal();
        }
    }
}