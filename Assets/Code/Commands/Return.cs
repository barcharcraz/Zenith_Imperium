using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    class Return : ITargetedCommand<IResourceDrop>
    {
        private ClickEventHandler m_handler;
        public void exec(BasicController controller, IResourceDrop target)
        {
            if (controller.Info is PeonInfo)
            {
                PeonInfo info = controller.Info as PeonInfo;
                controller.Owner.HarvestedResources += info.StoredResources;
                info.StoredResources -= info.StoredResources;
            }
            else
            {
                throw new InvalidOperationException("the return command can only be added to Peons");
            }
        }

        public string Name
        {
            get { return "Return"; }
        }

        public void exec(BasicController controller)
        {
            m_handler = (object sender, ClickEventArgs e) => Owner_SendCommand(sender, e, controller);
            controller.Owner.SendCommand += m_handler;
        }
        void Owner_SendCommand(object sender, ClickEventArgs e, BasicController b)
        {
            Collider[] hits = Physics.OverlapSphere(e.worldPos, 1);
            IResourceDrop cont = null;
            foreach (Component c in hits)
            {
                if (c.GetComponent<BasicController>().Info is IResourceDrop)
                {
                    cont = c.GetComponent<BasicController>().Info as IResourceDrop;
                    break;
                }
            }
            if (cont != null)
            {
                exec(b,cont);
                b.Owner.SendCommand -= m_handler;
            }
        }
    }
}
