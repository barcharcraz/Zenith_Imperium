using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Units;
using System.Text;
using UnityEngine;

namespace Commands
{
    abstract class TargetedCommandImp<T> : ITargetedCommand<T> where T : class
    {
        protected ClickEventHandler m_handler;
        public void exec(BasicController controller)
        {
            //bind the handler to a compitible type by supplying the final
            //argument from our argument list
            m_handler = (object sender, ClickEventArgs e) => Owner_SendCommand(sender, e, controller);
            controller.Owner.SendCommand += m_handler;
        }
        protected virtual void Owner_SendCommand(object sender, ClickEventArgs e, BasicController b)
        {

            //Find things near where the player clicked
            //1 is small enough that that is likely the thing that the player meant
            //to click on
            Collider[] hits = Physics.OverlapSphere(e.worldPos, 1);
            Component cont = null;
            foreach (Component c in hits)
            {
                cont = c.GetComponent(typeof (T));
                //break as soon as we find something with the right script
                //no need to keep on searching
                if (cont != null)
                {
                    break;
                }
            }
            //if we found a component of the right type
            if (cont != null)
            {
                exec(b,cont as T);
                b.Owner.SendCommand -= m_handler;
            }
        }
        public abstract void exec(BasicController controller, T target);

        public abstract string Name { get; }
    }
}
