using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
	public class CommandManager
	{
        private GameObject m_attachedObject;
        Queue<Command> m_commandQueue;
        public CommandManager(GameObject target)
        {
            m_attachedObject = target;
            m_commandQueue = new Queue<Command>();
        }
        public void handleCommand()
        {

        }
        
	}
}
