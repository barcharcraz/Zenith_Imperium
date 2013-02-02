using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    public class ProduceUnit<T> : Command<BasicController> where T : IUnitInfo, new()
    {
        private T m_unit;

        public ProduceUnit(T unit)
        {
            m_unit = unit;
        }
        public ProduceUnit() { }
        public override void exec(BasicController controller)
        {
            if (m_unit == null)
            {
                m_unit = new T();
            }
            m_unit.CreateUnit(controller.Owner, controller.transform.position + new Vector3(10,0,0), Quaternion.identity);
            
        }
        public override void initCommand()
        {
            if (m_unit == null)
            {
                m_unit = new T();
            }
        }
        public override string Name
        {
            get { return m_unit.Name; }
        }
    }
}
