using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public interface ICommandBase
    {
        string Name { get; }
        void preExec(BasicController controller);
        bool exec(BasicController controller);
        void postExec(BasicController controller);
        void initCommand();
    }
}
