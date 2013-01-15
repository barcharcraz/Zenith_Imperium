using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    class Harvest : ITargetedCommand<ResourceController>
    {

        public void exec(UnitController controller, ResourceController target)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return "Harvest"; }
        }

        public void exec(BasicController controller)
        {
            throw new NotImplementedException();
        }
    }
}
