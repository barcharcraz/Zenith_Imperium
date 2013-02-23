using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Commands
{
    public class ProduceUnit<T> : Command<BasicController>, ITimedCommand<BasicController>  where T : IUnitInfo, new()
    {
        private T m_unit;
        private float m_remainingTime;
        private bool m_running;
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
            controller.StartCoroutine(coExec(controller));
            
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

        public IEnumerator coExec(BasicController controller)
        {
            m_running = true;
            m_remainingTime = m_unit.constructionTime;
            while (m_remainingTime > 0)
            {
                yield return new WaitForSeconds(1);
                m_remainingTime -= 1;
            }
            m_unit.CreateUnit(controller.Owner, controller.transform.position + new Vector3(10, 0, 0), Quaternion.identity);
        }

        public float RemainingTime
        {
            get { return m_remainingTime; }
        }

        public bool Running
        {
            get { return m_running; }
        }
    }
}
