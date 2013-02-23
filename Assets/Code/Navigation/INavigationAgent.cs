using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navigation
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Navigation agent interface, designed to 
    ///             wrap the required navigation functionality so that
    ///             one can substitute in alternative navigation implementations </summary>
    ///
    /// <remarks>   Charlie, 2/23/2013. </remarks>
    ///-------------------------------------------------------------------------------------------------
    interface INavigationAgent
    {
        Vector3 Destination { get; set; }
        bool isNavigateing { get; }
    }
}
