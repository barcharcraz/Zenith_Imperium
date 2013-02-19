using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;
namespace Commands
{
    class Move : Command<UnitController>, ITargetedCommand<Vector3, UnitController>
    {
        private UnitController m_controller;
        private MouseEventHandler m_handler;
        public Move()
        {
            m_handler = WaitForInput;
        }
        public override string Name
        {
            get { return "Move"; }
        }

        public override void exec(UnitController controller)
        {
            m_controller = controller;
            controller.Owner.SendCommand += m_handler;
            
        }
        private void WaitForInput(object sender, MouseEventArgs e)
        {
            m_controller.moveTo(e.worldPos, 0);
            m_controller.Owner.SendCommand -= m_handler;
            
        }

        public void exec(UnitController controller, Vector3 target)
        {
            controller.StartCoroutine(controller.coMoveTo(target, 20));
        }
    }
}
