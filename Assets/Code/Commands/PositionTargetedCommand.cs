using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;

namespace Commands
{
    public abstract class PositionTargetedCommand<U> : TargetedCommandImp<Vector3, U> where U : BasicController
    {


        

        protected override Vector3 GetTarget(Vector3 clickPos)
        {
            //this is just a pass through since we actually want the position
            return clickPos;
        }

    }
}
