using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    class Build : PositionTargetedCommand<BasicController>
    {
        public override void exec(BasicController controller, UnityEngine.Vector3 target)
        {
            
        }

        public override string Name
        {
            get { return "build Test"; }
        }
    }
}
