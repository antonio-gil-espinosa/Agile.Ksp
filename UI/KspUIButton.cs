using System;
using UnityEngine;

namespace Agile.Ksp.UI
{
    public class KspUIButton : KspUIControl
    {
        private readonly Action _callback;
        public string Caption { get; set; }
        public Texture Texture { get; set; }

        public KspUIButton(string caption, Action callback)
        {
            _callback = callback;
            Caption = caption;
        }

        public KspUIButton(Texture texture, Action callback)
        {
            _callback = callback;
            Texture = texture;
        }

        public override void Draw()
        {
            if (Caption != null && GUILayout.Button(Caption)) //GUILayout.Button is "true" when clicked
                _callback();
            else if (Texture != null && GUILayout.Button(Texture)) //GUILayout.Button is "true" when clicked
                _callback();
        }
    }
}