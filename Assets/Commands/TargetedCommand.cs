using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
namespace Commands
{
    public abstract class TargetedCommand<T> : ITargetedCommand<T>
    {


        private BasicController m_controller;
        protected abstract T getTarget(ClickEventArgs e);
        public void exec(BasicController controller)
        {
            controller.Owner.SendCommand += Owner_SendCommand;
        }

        void Owner_SendCommand(object sender, ClickEventArgs e)
        {
            exec()
        }
    }
}
