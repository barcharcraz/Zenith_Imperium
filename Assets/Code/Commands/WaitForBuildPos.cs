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
        private GameObject m_ghost;
        private Vector3 retval;
        public override Type[] ReturnType
        {
            get
            {
                return new Type[] { typeof(Vector3) };
            }
        }
        public WaitForBuildPos()
        {
            m_info = new T();
            m_ghost = m_info.CreateGhost(new Vector3(), Quaternion.identity);
        }
        public override void Update()
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(screenRay, out hitInfo);
            retval = hitInfo.point;
            m_ghost.transform.position = retval;
            if (Input.GetButtonDown("Select"))
            {
                GameObject.Destroy(m_ghost);
                OnFinished(retval);
            }
        }
    }
}
