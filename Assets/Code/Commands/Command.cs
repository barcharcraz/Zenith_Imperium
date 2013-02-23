using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public abstract class Command<T> : ICommand<T> where T : BasicController
    {

        public abstract void exec(T controller);

        public abstract string Name { get; }

        public virtual void initCommand() { }
        
        void ICommandBase.exec(BasicController controller)
        {
            exec(controller as T);
        }
    }
}
