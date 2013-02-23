using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navigation
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   This is a special kind of collection that is indexed by double
    ///             value instead of by integer value.
    ///             This is used to store height-fields that correspond to positions on
    ///             a x,y plane </summary>
    ///
    /// <remarks>   Charlie, 2/11/2013. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class HeightField
    {
        private HeightPoint[,] array;
        private int m_resolution;
        private Texture2D m_bitmapCache;
        private bool m_invalidate;
        public int Length
        {
            get { return array.Length; } 
        }
        public int GetLength(int dim)
        {
            return array.GetLength(dim)/m_resolution;
        }
        public HeightField(int resolution, int sizeX, int sizeY)
        {
            m_resolution = resolution;
            array = new HeightPoint[resolution * sizeX, resolution * sizeY];
            m_invalidate = false;
            
        }
        public HeightPoint this[int x, int y]
        {
            get
            {
                return array[x,y];
            }
            set
            {
                array[x,y] = value;
                m_invalidate = true;
            }
        }
        
        public HeightPoint getHeightAtPosition(Vector2 pos)
        {
            HeightPoint retval = array[(int)Math.Round(pos.x * m_resolution), (int)Math.Round(pos.y * m_resolution)];
            //check if the HeightPoint in a reasonable array position is at the right position, avoids a slow lookup
            if (retval.position2 == pos)
            {
                return retval;
            }
            else
            {
                //looks like the point was not in the expected place, go on and find it
                IEnumerable<HeightPoint> coll = from HeightPoint p in array where p.position2 == pos select p;
                return coll.ElementAt(0);
            }

        }
        public HeightPoint getHeightAtPosition(float x, float y)
        {
            return getHeightAtPosition(new Vector2(x, y));
        }
        public Texture2D getBitmap(float maxHeight = 300)
        {
            if (m_bitmapCache == null || m_invalidate)
            {
                Texture2D retval = new Texture2D(array.GetLength(0), array.GetLength(1), TextureFormat.ARGB32, false);
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    for (int y = 0; y < array.GetLength(1); y++)
                    {
                        retval.SetPixel(x, y, new Color((getHeightAtPosition(x,y).height) / maxHeight, 0, 0));
                    }
                }
                retval.Apply();
                m_bitmapCache = retval;
                m_invalidate = false;
                return retval;
            }
            else
            {
                return m_bitmapCache;
            }
            
        }

        
    }
}
