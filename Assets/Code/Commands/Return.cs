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
    class Return : ComponentTargetedCommand<BasicController, IResourceDrop, UnitController>
    {
        //temp will remove this when I implement a more elegant command
        private bool m_finished = false;
        private bool first = true;
        public override bool exec(UnitController controller, BasicController target)
        {
            if (first)
            {
                controller.StartCoroutine(coReturn(controller, target));
                first = false;
            }
            return m_finished;
        }
        private IEnumerator coReturn(UnitController controller, BasicController target)
        {
            if (controller.Info is PeonInfo)
            {
                PeonInfo info = controller.Info as PeonInfo;
                Vector3 targetPos = target.transform.position;
                yield return controller.StartCoroutine(controller.coMoveTo(targetPos, base.Deltad));
                //if the controller has a navmesh agent then go ahead
                //and try and move it to the drop point
                controller.Owner.HarvestedResources += info.StoredResources;
                info.StoredResources -= info.StoredResources;
                m_finished = true;
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
