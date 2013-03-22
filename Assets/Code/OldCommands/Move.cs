using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;
namespace Commands
{
    class Move : PositionTargetedCommand<UnitController>
    {
        public override string Name
        {
            get { return "Move"; }
        }

        

        public override bool exec(UnitController controller, Vector3 target)
        {
            return controller.moveTo(target, 1);
        }
    }
}
