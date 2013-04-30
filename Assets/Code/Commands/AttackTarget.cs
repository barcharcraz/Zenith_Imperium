using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    class AttackTarget : Command
    {
        private BasicController m_parent;
        private BasicController m_target;
        private CommandManager m_parentController;
        public AttackTarget(CommandManager parent, BasicController target)
        {
            m_parent = parent.ParentController;
            m_target = target;
            m_parentController = parent;
        }
        public override void Update()
        {
            Vector3 targetPos = m_target.transform.position;
            Vector3 parentPos = m_parent.transform.position;
            if (Vector3.Distance(parentPos, targetPos) < m_parent.Info.AttackRange)
            {
                m_target.Info.CurrHealth -= m_parent.Info.AttackPower;
            }
            else
            {
                //TODO: make this shorter
                m_parentController.AddCommandNow(new MoveInRange(m_parent, m_target, m_parent.Info.AttackRange));
            }
        }
    }
}
