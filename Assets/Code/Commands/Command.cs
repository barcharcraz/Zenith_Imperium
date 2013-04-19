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
		public virtual Type[] ReturnType { get { return new Type[]{}; } }
		//public CommandManager parent { get; set; }
		protected virtual void OnFinished(params System.Object[] retval)
		{
			Finished(this, retval);
		}
		public virtual void Init() {}
		public abstract void Update();
		
		protected virtual void OnAddCommands(params Type[] commands)
		{
			AddCommands(commands);
		}
		/// <summary>
		/// called when the command is executing and the unit is selected, for making the
		/// information about the command display in the unit area
		/// 
		/// Called from OnGUI so feel free to use gui functions
		/// </summary>
		public virtual void OnDraw()
		{

		}

	}
}
