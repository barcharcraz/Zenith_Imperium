using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    class Return : TargetedCommandImp<IResourceDrop>
    {
        public override void exec(BasicController controller, IResourceDrop target)
        {
            if (controller.Info is PeonInfo)
            {
                PeonInfo info = controller.Info as PeonInfo;
                Vector3 targetPos = (target as Component).transform.position;
                NavMeshAgent agent = controller.GetComponent<NavMeshAgent>();
                //if the controller has a navmesh agent then go ahead
                //and try and move it to the drop point
                if (agent != null)
                {
                    agent.destination = targetPos;
                }
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
