using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    public class ProduceUnit : ICommand
    {
        private UnitInfo m_unit;

        public ProduceUnit(UnitInfo unit)
        {
            m_unit = unit;
        }

        public void exec(BasicController controller)
        {
            controller.Owner.HarvestedResources -= m_unit.Cost;
            m_unit.CreateUnit(controller.Owner, controller.transform.position + new Vector3(10,0,0), Quaternion.identity);
            
        }

        public string Name
        {
            get { return m_unit.Name; }
        }
    }
}
