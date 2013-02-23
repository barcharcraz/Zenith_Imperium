using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;

namespace Commands
{
    interface ITimedCommand<T> : ICommand<T> where T : BasicController
    {
        float RemainingTime { get; }
        bool Running { get; }
    }
}
