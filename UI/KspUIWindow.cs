using UnityEngine;

namespace Agile.Ksp.UI
{
    public class KspUIWindow : KspUIContainer
    {
        private readonly Part _part;

        public virtual bool Visible { get; protected set; }
        public string Title { get; set; }

        public KspUIWindow(Part part)
        {
            _part = part;
            GameEvents.onPartDestroyed.Add(Close);
        }

        private void Close(Part part)
        {
            if (_part != part)
                return;

            Close();
            GameEvents.onPartDestroyed.Remove(Close);
        }

        public KspUIWindow()
        {
        }

        public void Show()
        {
            if (!Visible)
            {
                RenderingManager.AddToPostDrawQueue(3, Draw);
                Visible = true;
            }
        }

        public void Close()
        {
            if (Visible)
            {
                RenderingManager.RemoveFromPostDrawQueue(3, Draw);
                Visible = false;
            }
        }

        public Rect Position { get; set; }

        public override void Draw()
        {
            if (_part == null || _part.vessel == FlightGlobals.ActiveVessel ||
                Vector3.Distance(FlightGlobals.ActiveVessel.transform.position, _part.transform.position) <= 2)
            {
                GUI.skin = HighLogic.Skin;
                Position = GUILayout.Window(1, Position, OnDraw, Title, GUILayout.MinWidth(100));
            }
            else
                Close();
        }

        private void OnDraw(int id)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
            foreach (KspUIControl control in Controls)
            {
                control.Draw();
            }
        }
    }
}