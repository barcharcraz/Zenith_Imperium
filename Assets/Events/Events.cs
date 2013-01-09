using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    public delegate void ClickEventHandler(object sender, ClickEventArgs e);
    public sealed class ClickEventArgs
    {
        public float mouseX;
        public float mouseY;
        public UnityEngine.Vector3 worldPos;
        public ClickEventArgs(UnityEngine.Vector3 mousePos) : this(mousePos, new UnityEngine.Vector3()) {}
        public ClickEventArgs(UnityEngine.Vector3 mousePos, UnityEngine.Camera cam) : this(mousePos, cam.ScreenToWorldPoint(mousePos)) { }
        public ClickEventArgs(UnityEngine.Vector3 mousePos, UnityEngine.Vector3 wpos)
        {
            mouseX = mousePos.x;
            mouseY = mousePos.y;
            worldPos.x = wpos.x;
            worldPos.y = wpos.y;
            worldPos.z = wpos.z;
        }
        
        
    }
}
