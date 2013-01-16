using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;
using Units;

namespace Commands
{
    class Harvest : ITargetedCommand<ResourceController>
    {
        private ClickEventHandler m_handler;
        public void exec(BasicController controller, ResourceController target)
        {
            if (!(controller.Info is PeonInfo))
            {
                throw new System.InvalidOperationException("The harvest command can only be added to Peons");
            }
            else
            {

            }
        }
        IEnumerator doHarvest(ResourceController source, Pe)
        public string Name
        {
            get { return "Harvest"; }
        }

        public void exec(BasicController controller)
        {
            m_handler = (object sender, ClickEventArgs e) => Owner_SendCommand(sender, e, controller);
            controller.Owner.SendCommand += m_handler;
        }

        void Owner_SendCommand(object sender, Events.ClickEventArgs e, BasicController b)
        {
            Collider[] hits = Physics.OverlapSphere(e.worldPos, 1);
            ResourceController cont = null;
            foreach (Component c in hits)
            {
                if (c.GetComponent<ResourceController>())
                {
                    cont = c.GetComponent<ResourceController>();
                    break;
                }
            }
            if (cont != null)
            {

                exec(b, cont);
                b.Owner.SendCommand -= m_handler;
            }
            
        }
    }
}
