using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public delegate void CommandFinishedHandler(Command src, System.Object retval);
    public delegate void CommandAddHandler(params Type[] commands);
	public class Command : MonoBehaviour
	{
        public event CommandAddHandler AddCommands;
        public event CommandFinishedHandler Finished;
        public virtual void Awake()
        {
            this.enabled = false;
        }
        public virtual void init(System.Object args)
        {
            this.enabled = true;
        }
        protected virtual void OnFinished(System.Object retval)
        {
            Finished(this, retval);
            
            
        }
        protected virtual void OnAddCommands(params Type[] commands)
        {
            AddCommands(commands);
        }
        public virtual void OnDestroy()
        {
            OnFinished(null);
        }
	}
}
