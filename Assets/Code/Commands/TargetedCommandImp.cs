using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Units;
using System.Text;
using UnityEngine;

namespace Commands
{
    public abstract class TargetedCommandImp<T,U> : Command<U>, ITargetedCommand<T,U> where U : BasicController
    {
        protected float Deltad
        {
            get { return 10; }
        }
        //TODO this flag is retarded and should be killed with fire
        private bool hasExecuted = false;
        private T target = default(T);
        protected MouseEventHandler m_handler;
        public override bool exec(U controller)
        {
            // Only bind the event handler on the first run of exec,
            // since we assign a lambda to m_handler inside this if and only inside thei if
            // we know that if m_handler is null it is the first run
            if (m_handler == null)
            {
                //bind the handler to a compitible type by supplying the final
                //argument from our argument list
                m_handler = (object sender, MouseEventArgs e) => Owner_SendCommand(sender, e, controller);
                controller.Owner.SendCommand += m_handler;
            }
            return execThunk(controller);
        }
        protected virtual void Owner_SendCommand(object sender, MouseEventArgs e, U b)
        {
            
            //if we found a component of the right type
            target = GetTarget(e.worldPos);
            hasExecuted = true;
            b.Owner.SendCommand -= m_handler;
            
        }
        protected abstract T GetTarget(Vector3 clickPos);
        private bool execThunk(U controller)
        {
            if (hasExecuted)
            {
                
                m_handler = null;
                return exec(controller, target);
            }
            else
            {
                return false;
            }
        }
        public abstract bool exec(U controller, T target);

        public override abstract string Name { get; }
    }
}
