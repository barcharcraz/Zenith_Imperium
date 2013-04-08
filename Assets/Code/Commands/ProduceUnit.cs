using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands
{
	class ProduceUnit<T> : Command where T : IUnitInfo, new()
	{
		private IUnitInfo m_info;
		private float m_remainingTime;
		public float RemainingTime { get { return m_remainingTime; } }

		public ProduceUnit()
		{
			m_info = new T();
			m_remainingTime = m_info.constructionTime;
		}
		public override void Update()
		{
			m_remainingTime -= Time.deltaTime;
			if (RemainingTime <= 0)
			{
				m_info.CreateUnit(parent.GetComponent<BasicController>().Owner, parent.transform.position + parent.transform.right*5, Quaternion.identity);
                OnFinished(null);
			}
		}

	}
}
