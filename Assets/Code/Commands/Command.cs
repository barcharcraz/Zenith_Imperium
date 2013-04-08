using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
	public delegate void CommandFinishedHandler(Command src, params System.Object[] retval);
	public delegate void CommandAddHandler(params Type[] commands);
	public abstract class Command
	{
		public event CommandAddHandler AddCommands;
		public event CommandFinishedHandler Finished;
		//specify the type signature of the returned values
		public virtual Type[] ReturnType { get { return null; } }
		public CommandManager parent { get; set; }
		protected virtual void OnFinished(System.Object retval)
		{
			Finished(this, retval);
		}
		public abstract void Update();
		
		protected virtual void OnAddCommands(params Type[] commands)
		{
			AddCommands(commands);
		}

	}
}
