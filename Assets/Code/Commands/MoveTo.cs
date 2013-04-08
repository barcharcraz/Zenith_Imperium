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
		public MoveTo(Vector3 target)
		{
			m_target = target;
		}

        public void Start()
        {
            //find the navagation agent and set the destination
            parent.GetComponent<NavMeshAgent>().destination = m_target;
        }
        public override void Update()
        {
            if (parent.GetComponent<NavMeshAgent>().hasPath == false)
            {
                OnFinished(null);
            }
        }
	}
}
