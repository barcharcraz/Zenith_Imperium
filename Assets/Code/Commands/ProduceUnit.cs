using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands
{
	class ProduceUnit : Command
	{
        private IUnitInfo m_info;
        private float m_remainingTime;
        public float RemainingTime { get { return m_remainingTime; } }

        public ProduceUnit(IUnitInfo info)
        {
            m_info = info;
            m_remainingTime = m_info.constructionTime;
        }
        public void Update()
        {
            m_remainingTime -= Time.deltaTime;
            if (RemainingTime <= 0)
            {
                m_info.CreateUnit(GetComponent<BasicController>().Owner, transform.right * 5, Quaternion.identity);
                Destroy(this);
            }
        }

	}
}
