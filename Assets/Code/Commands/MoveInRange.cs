using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class MoveInRange : Command
    {
        private BasicController m_parent;
        private BasicController m_target;
        private NavMeshAgent m_agent;
        private float m_range;
        public MoveInRange(BasicController parent, BasicController target, float range)
        {
            m_parent = parent;
            m_target = target;
            m_range = range;
            m_agent = m_parent.GetComponent<NavMeshAgent>();
        }
        public override void Update()
        {
            if (getDistance() < m_range)
            {
                OnFinished();
            }
            else
            {
                m_agent.destination = m_target.transform.position;
            }
        }
        private float getDistance()
        {
            return Vector3.Distance(m_parent.transform.position, m_target.transform.position);
        }
    }
}
