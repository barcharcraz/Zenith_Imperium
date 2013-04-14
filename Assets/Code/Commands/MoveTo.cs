using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
	public class MoveTo : Command
	{
        private Vector3 m_target;
        private CommandManager m_parent;
		public MoveTo(CommandManager parent,Vector3 target)
		{
			m_target = target;
            m_parent = parent;
			//find the navagation agent and set the destination
            m_parent.GetComponent<NavMeshAgent>().destination = m_target;
		}

        public override void Update()
        {
            if (m_parent.GetComponent<NavMeshAgent>().hasPath == false)
            {
                OnFinished();
            }
        }
	}
}
