using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    interface ITargetedCommand<T> : ICommand
    {
        void exec(UnitController controller, T target);
    }
}
