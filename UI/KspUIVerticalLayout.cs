using UnityEngine;

namespace Agile.Ksp.UI
{
    public class KspUIVerticalLayout : KspUIContainer
    {
        public override void Draw()
        {
            GUILayout.BeginVertical();
            base.Draw();
            GUILayout.EndVertical();
        }
    }
}