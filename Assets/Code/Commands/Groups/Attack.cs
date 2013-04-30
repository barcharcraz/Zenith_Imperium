using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Commands.Groups
{
    class Attack : Command
    {
        private CommandManager m_parent;
        public Attack(CommandManager parent)
        {
            m_parent = parent;
        }
        public override void Update()
        {
            m_parent.AddCommands(typeof(WaitForClick), typeof(GetUnitAt), typeof(AttackTarget));
            OnFinished();
        }
    }
}
