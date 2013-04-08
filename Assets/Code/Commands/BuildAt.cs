using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands
{
	class BuildAt<T> : Command where T : IUnitInfo, new()
	{
		private T m_info;
		private Vector3 m_target;
		public BuildAt(Vector3 target)
		{
			m_target = target;
		}
		public virtual void Start()
		{
			m_info = new T();
		}
		public override void Update()
		{
			
			m_info.CreateUnit(parent.GetComponent<BasicController>().Owner, m_target, Quaternion.identity);
			OnFinished(null);
		}
	}
}
