using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Units;
using UnityEngine;

namespace Commands
{
    class Build<T> : PositionTargetedCommand<BasicController> where T : UnitInfo, new()
    {
        protected MouseEventHandler m_moveHandler;
        private T m_unit;
        private GameObject m_ghost;
        public Build() { }
        public override void exec(BasicController controller, UnityEngine.Vector3 target)
        {
            m_ghost.SetActive(false);
            m_unit.CreateUnit(controller.Owner, target, Quaternion.identity);
        }
        public override void exec(BasicController controller)
        {
            base.exec(controller);
            
            m_ghost.SetActive(true);
            controller.Owner.MouseMove += Owner_MouseMove;

        }
        public override void initCommand()
        {
            if (m_unit == null)
            {
                m_unit = new T();
            }
            m_ghost = m_unit.CreateGhost(Vector3.zero, Quaternion.identity, false);

        }
        void Owner_MouseMove(object sender, MouseEventArgs e)
        {
            m_ghost.transform.position = e.worldPos;
            
        }
        public override string Name
        {
            get { return  "Build " + m_unit.Name; }
        }
    }
}
