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
    public class Harvest : ComponentTargetedCommand<ResourceController, UnitController>
    {
        //this is how far away to start harvesting, the peon will actually stop here
        private const float DELTAD = 30;
        private bool isDone = false;
        private bool first = true;
        public override bool exec(UnitController controller, ResourceController target)
        {
            if (first)
            {
                if (!(controller.Info is PeonInfo))
                {
                    throw new System.InvalidOperationException("The harvest command can only be added to Peons");
                }
                else
                {
                    controller.StartCoroutine(doHarvest(target, controller));
                    first = false;
                }
            }
            return isDone;
        }
        IEnumerator doHarvest(ResourceController sourceCont, UnitController peonCont)
        {
            //wait until the peon is in the right position to actually
            //start harvesting
            while (!peonCont.moveTo(sourceCont.transform.position, DELTAD))
            {
                yield return null;
            }
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
            isDone = true;
        }

        public override string Name
        {
            get { return "Harvest"; }
        }
    }
}
