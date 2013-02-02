using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public interface ICommandBase
    {
        string Name { get; }
        void exec(BasicController controller);
    }
}
