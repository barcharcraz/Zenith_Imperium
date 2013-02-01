using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using System.Text;
using System.Collections;
using Units;
using UnityEngine;

namespace Commands
{
    class Return : TargetedCommandImp<IResourceDrop, UnitController>
    {
        public override void exec(UnitController controller, IResourceDrop target)
        {
            controller.StartCoroutine(coReturn(controller, target));
        }
        private IEnumerator coReturn(UnitController controller, IResourceDrop target)
        {
            if (controller.Info is PeonInfo)
            {
                PeonInfo info = controller.Info as PeonInfo;
                Vector3 targetPos = (target as Component).transform.position;
                yield return controller.StartCoroutine(controller.coMoveTo(targetPos, base.Deltad));
                //if the controller has a navmesh agent then go ahead
                //and try and move it to the drop point
                controller.Owner.HarvestedResources += info.StoredResources;
                info.StoredResources -= info.StoredResources;
            }
            else
            {
                throw new InvalidOperationException("the return command can only be added to Peons");
            }
            
        }
        public override string Name
        {
            get { return "Return"; }
        }
    }
}
