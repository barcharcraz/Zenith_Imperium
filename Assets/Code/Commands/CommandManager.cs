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
			//we dont want to be running update while we are in this part of the code
			//not using active since this will change but the end of the block anyways
			executingCommand = null;
			if (src != null)
			{
				src.Finished -= handleCommand;
				src.AddCommands -= AddCommands;
			}
			//we dont want to proceed to the next command if there isnt one
			if(m_commandQueue.Count > 0) {
				Type nextCommand = m_commandQueue.Dequeue();
			
				if (nextCommand.BaseType != typeof(Commands.Command))
				{
					throw new InvalidOperationException("Commands must inherit from Command type " + nextCommand + "does not");
				}
				IEnumerable<ConstructorInfo> possibleConst = GetCompatibleConstructors(nextCommand, src);
				Command nextCommandinst = possibleConst.First().Invoke(args) as Command;
				nextCommandinst.parent = this;
				nextCommandinst.Finished += handleCommand;
				nextCommandinst.AddCommands += AddCommands;
				//aaaaand start executing the new command
				executingCommand = nextCommandinst;
			}
			
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
			//only want to kick off execution if we are not already executing anything, otherwise that previous thing can continue on its way
			if (executingCommand==null)
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

		private IEnumerable<ConstructorInfo> GetCompatibleConstructors(Type command, Command prev)
		{
			//we want to get an empty constructor if there was no prev command
			if(prev == null)
			{
				return GetCompatibleConstructors(command);
			} else {
				return GetCompatibleConstructors(command, prev.ReturnType);
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
