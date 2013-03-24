using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace Commands
{
	public class CommandManager
	{
        private GameObject m_attachedObject;
        private Queue<Type> m_commandQueue;
        public CommandManager(GameObject target)
        {
            m_attachedObject = target;
            m_commandQueue = new Queue<Type>();
        }
        public void handleCommand(Command src, System.Object args)
        {
            src.Finished -= handleCommand;
            src.AddCommands -= AddCommands;
            Type nextCommand = m_commandQueue.Dequeue();
            if (!nextCommand.IsAssignableFrom(typeof(Command)))
            {
                throw new InvalidOperationException("Commands must inherit from Command type " + nextCommand + "does not");
            }
            
            Command addedCommand = m_attachedObject.AddComponent(nextCommand) as Command;
            addedCommand.init(args);
            addedCommand.Finished += handleCommand;
            addedCommand.AddCommands += AddCommands;
            
        }
        public void AddCommands(params Type[] commands)
        {
            foreach (Type c in commands)
            {
                m_commandQueue.Enqueue(c);
            }
        }
        
	}
}
