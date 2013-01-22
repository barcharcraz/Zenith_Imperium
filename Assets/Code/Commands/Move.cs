using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;
namespace Commands
{
    class Move : ITargetedCommand<Vector3>
    {
        private BasicController m_controller;
        private ClickEventHandler m_handler;
        public Move()
        {
            m_handler = WaitForInput;
        }
        public string Name
        {
            get { return "Move"; }
        }

        public void exec(BasicController controller)
        {
            m_controller = controller;
            controller.Owner.SendCommand += m_handler;
            
        }
        private void WaitForInput(object sender, ClickEventArgs e)
        {
            try
            {
                m_controller.GetComponent<NavMeshAgent>().SetDestination(e.worldPos);
                
                
            }
            catch (NullReferenceException)
            {
                throw new System.InvalidOperationException("Unit Controllers for movable units must have a navmeshagent component");
            }
            m_controller.Owner.SendCommand -= m_handler;
            
        }

        public void exec(BasicController controller, Vector3 target)
        {
            controller.GetComponent<NavMeshAgent>().SetDestination(target);
        }
    }
}
