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
        private double[,] array;
        private int m_resolution;
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
            array = new double[resolution * sizeX, resolution * sizeY];
            
        }
        public double this[double x, double y]
        {
            get
            {
                return array[(int)Math.Round(x * m_resolution), (int)Math.Round(y * m_resolution)];
            }
            set
            {
                array[(int)Math.Round(x * m_resolution), (int)Math.Round(y * m_resolution)] = value;
            }
        }
        public Texture2D getBitmap(float maxHeight = 300)
        {
            Texture2D retval = new Texture2D(array.GetLength(0), array.GetLength(1), TextureFormat.ARGB32, false);
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    retval.SetPixel(x, y, new Color(((float)array[x, y]) / maxHeight, 0, 0));
                }
            }
            retval.Apply();
            return retval;
        }

        
    }
}
