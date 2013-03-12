using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public abstract class Command<T> : ICommand<T> where T : BasicController
    {

        public abstract bool exec(T controller);
        public virtual void preExec(T controller) { }
        public virtual void postExec(T controller) { }

        public abstract string Name { get; }

        public virtual void initCommand() { }
        
        bool ICommandBase.exec(BasicController controller)
        {
            return exec(controller as T);
        }
        void ICommandBase.preExec(BasicController controller)
        {
            preExec(controller as T);
        }
        void ICommandBase.postExec(BasicController controller)
        {
            postExec(controller as T);
        }
    }
}
