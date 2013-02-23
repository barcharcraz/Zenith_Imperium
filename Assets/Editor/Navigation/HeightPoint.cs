using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navigation
{
    public class HeightPoint
    {
        public Vector3 position3;
        public Vector2 position2
        {
            get { return new Vector2(position3.x, position3.z); }
        }
        public float height
        {
            get { return position3.y; }
            set { position3.y = value; }
        }
        public bool walkable = true;
    }
}
