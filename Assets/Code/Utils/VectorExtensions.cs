using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExtensionMethods
{
    public static class VectorExtensions
    {
        /// <summary>
        /// gets the max component of a vector
        /// </summary>
        /// <param name="vec">the vector to get data from</param>
        /// <returns>the value of the biggest component of the vector</returns>
        public static float GetMaxComponent(this Vector3 vec)
        {
            float retval = vec.x;
            if (vec.y > retval)
            {
                retval = vec.y;
            }
            if (vec.z > retval)
            {
                retval = vec.z;
            }
            return retval;
        }
        /// <summary>
        /// get the smallest component in Vector3 <paramref name="vec"/>
        /// </summary>
        /// <param name="vec">the vector to calculate the smallest component of</param>
        /// <returns>the value of the smallest component of the vector</returns>
        public static float GetMinComponent(this Vector3 vec)
        {
            float retval = vec.x;
            if (vec.y < retval)
            {
                retval = vec.y;
            }
            if (vec.z < retval)
            {
                retval = vec.z;
            }
            return retval;
        }
        public static bool IsStrictlyGreaterThan(this Vector3 lhs, Vector3 rhs)
        {

            if (lhs.x >= rhs.x && lhs.z <= rhs.z)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static bool IsStrictlyLessThan(this Vector3 lhs, Vector3 rhs)
        {
            if (lhs.x <= rhs.x && lhs.z >= rhs.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
