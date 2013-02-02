using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Units;
using UnityEngine;

namespace Commands
{
    class Build<T> : PositionTargetedCommand<BasicController> where T : IUnitInfo, new()
    {
        protected MouseEventHandler m_moveHandler;
        private T m_unit;
        public override void exec(BasicController controller, UnityEngine.Vector3 target)
        {
            m_unit.CreateUnit(controller.Owner, target, Quaternion.identity);
        }
        public override void exec(BasicController controller)
        {
            base.exec(controller);
            controller.Owner.MouseMove += Owner_MouseMove;

        }

        void Owner_MouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override string Name
        {
            get { return "build Test"; }
        }
    }
}
