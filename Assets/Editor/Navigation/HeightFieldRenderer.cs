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
        private Terrain m_terrain;
        private Texture oldTexture;
        public Terrain SelectedTerrain
        {
            get { return m_terrain; }
            set { m_terrain = value; }
        }
        public HeightField ActiveField
        {
            get { return m_field; }
            set { m_field = value; }
        } 

        public HeightFieldRenderer(HeightField field, Terrain terrain)
        {
            m_field = field;
            m_terrain = terrain;
        }
        public void renderHeightField(bool enable)
        {
            if (enable)
            {
                
                oldTexture = m_terrain.renderer.material.mainTexture;
                m_terrain.renderer.material.mainTexture = m_field.getBitmap();
            }
            else
            {
                if (oldTexture != null)
                {
                    m_terrain.renderer.material.mainTexture = oldTexture;
                }
            }
        }
        

    }
}
