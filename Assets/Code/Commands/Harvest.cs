using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Units.MapFeatures;
using Events;
using Units;

namespace Commands
{
    class Harvest : TargetedCommandImp<ResourceController>
    {
        
        public override void exec(BasicController controller, ResourceController target)
        {
            if (!(controller.Info is PeonInfo))
            {
                throw new System.InvalidOperationException("The harvest command can only be added to Peons");
            }
            else
            {
                controller.StartCoroutine(doHarvest(target, controller));
            }
        }
        IEnumerator doHarvest(ResourceController sourceCont, BasicController peonCont)
        {
            ResourceNodeInfo source = sourceCont.Info;
            PeonInfo peon = peonCont.Info as PeonInfo;
            while (peon.StoredResources < peon.harvestAmount)
            {
                Resources load = source.CurrentResources.GetResources(1);
                peon.StoredResources += load;
                source.CurrentResources -= load;
                yield return new WaitForSeconds(peon.HarvestRate);
            }
        }

        public override string Name
        {
            get { return "Harvest"; }
        }
    }
}
