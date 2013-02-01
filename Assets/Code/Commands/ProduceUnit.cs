using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    public class ProduceUnit<T> : ICommand<BasicController> where T : IUnitInfo, new()
    {
        private T m_unit;

        public ProduceUnit(T unit)
        {
            m_unit = unit;
        }
        public ProduceUnit()
        {
            m_unit = new T();
        }
        public void exec(BasicController controller)
        {
            
            m_unit.CreateUnit(controller.Owner, controller.transform.position + new Vector3(10,0,0), Quaternion.identity);
            
        }

        public string Name
        {
            get { return m_unit.Name; }
        }
    }
}
