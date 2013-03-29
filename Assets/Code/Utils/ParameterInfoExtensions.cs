using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ExtensionMethods
{
	public static class ParameterInfoExtensions
	{
        /// <summary>
        /// Checks to see if an array of parameter infos all have the same type
        /// as an array of types
        ///
        /// in others words checks that the type signature of a parameter list
        /// matches an array of types
        /// </summary>
        /// <param name="pInfo">a lsit of parameter infos</param>
        /// <param name="types">a list of types</param>
        /// <returns>true if the parameter list type signature matches 
        /// <paramref name="types"/> and false otherwise</returns>
        public static bool IsEqual(this ParameterInfo[] pInfo, Type[] types)
        {
            if (pInfo.Length != types.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < pInfo.Length; ++i)
                {
                    if (pInfo[i].ParameterType != types[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
	}
}
