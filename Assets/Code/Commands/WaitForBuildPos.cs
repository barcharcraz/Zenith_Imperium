using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands
{
     
    class WaitForBuildPos<T> : Command where T : IUnitInfo, new()
    {
        private IUnitInfo m_info;
        private Vector3 m_target;
        private GameObject m_ghost;
        private Vector3 retval;
		public WaitForBuildPos(Vector3 target) 
		{
			m_target = target;
		}
        public virtual void Start()
        {
            m_info = new T();
            m_ghost = m_info.CreateGhost(new Vector3(), Quaternion.identity);
        }
        public override void Update()
        {
            m_ghost.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetButtonDown("Select"))
            {
                retval = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                OnFinished(retval);
            }
        }
    }
}
