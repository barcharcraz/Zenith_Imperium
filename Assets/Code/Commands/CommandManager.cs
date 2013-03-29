using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using ExtensionMethods;

namespace Commands
{
	public class CommandManager : MonoBehaviour
	{
		private Command executingCommand;
		private Queue<Type> m_commandQueue;
		public void Start() 
		{
			m_commandQueue = new Queue<Type>();
		}
		public void handleCommand(Command src, params System.Object[] args)
		{
			if (src != null)
			{
				src.Finished -= handleCommand;
				src.AddCommands -= AddCommands;
			}
			Type nextCommand = m_commandQueue.Dequeue();
			if (nextCommand.BaseType != typeof(Commands.Command))
			{
				throw new InvalidOperationException("Commands must inherit from Command type " + nextCommand + "does not");
			}
            IEnumerable<ConstructorInfo> possibleConst =  GetCompatibleConstructors(nextCommand, src.ReturnType);
            Command nextCommandinst = possibleConst.First().Invoke(args) as Command;
			nextCommandinst.Finished += handleCommand;
			nextCommandinst.AddCommands += AddCommands;
			
		}
		public void AddCommands(params Type[] commands)
		{
			foreach (Type c in commands)
			{
				AddCommand(c);
			}
		}
		public void AddCommand(Type command)
		{
			m_commandQueue.Enqueue(command);
			//only want to kick off if the previously added command is the only
			//command in the queue
			if (m_commandQueue.Count == 1)
			{
				handleCommand(null, null);
			}
			
		}
		public void Update() 
		{
			if(executingCommand != null) 
			{
				executingCommand.Update();
			}
		}
		private IEnumerable<ConstructorInfo> GetCompatibleConstructors(Type command, params Type[] signature)
		{
			IEnumerable<ConstructorInfo> retval = from ConstructorInfo c in command.GetConstructors()
												  where c.GetParameters().IsEqual(signature)
												  select c;
			return retval;
		}
		
		
	}
}
