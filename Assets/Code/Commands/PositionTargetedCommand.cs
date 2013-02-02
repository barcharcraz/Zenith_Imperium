using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;

namespace Commands
{
    public abstract class PositionTargetedCommand<U> : Command<U>, ITargetedCommand<Vector3, U> where U : BasicController
    {


        protected MouseEventHandler m_handler;
        public override void exec(U controller)
        {
            m_handler = (object sender, MouseEventArgs e) => Owner_SendCommand(sender, e, controller);
            controller.Owner.SendCommand += m_handler;
        }
        protected virtual void Owner_SendCommand(object sender, MouseEventArgs e, U b)
        {
            exec(b, e.worldPos);
            b.Owner.SendCommand -= m_handler;
        }
        public abstract void exec(U controller, Vector3 target);
        public override abstract string Name { get; }

    }
}
