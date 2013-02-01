using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    interface ITargetedCommand<T,U> : ICommand<U> where U : BasicController
    {
        
        void exec(U controller, T target);
    }
}
