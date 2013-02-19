using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Navigation
{
    public class HeightFieldRenderer
    {
        private HeightField m_field;
        public HeightFieldRenderer(HeightField field)
        {
            m_field = field;
        }
    }
}
