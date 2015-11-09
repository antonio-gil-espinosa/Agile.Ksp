using UnityEngine;

namespace Agile.Ksp.UI
{
    public class KspUIScroll : KspUIContainer
    {
        public override void Draw()
        {
            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, GUILayout.MinWidth(MinWidth),
                                                       GUILayout.MinHeight(MinHeight));
            base.Draw();

            GUILayout.EndScrollView();
        }

        public int MinWidth { get; set; } = 300;
        public int MinHeight { get; set; } = 300;

        public Vector2 ScrollPosition { get; private set; }
    }
}