using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands
{
     
    class Build<T> : Command where T : IUnitInfo, new()
    {
        private IUnitInfo m_info;
        private Vector3 m_target;
        private GameObject m_ghost;
        private Vector3 retval;
        public override void init(object args)
        {
            base.init(args);
            m_target = (Vector3)args;

        }
        public virtual void Start()
        {
            m_info = new T();
            m_ghost = m_info.CreateGhost(new Vector3(), Quaternion.identity);
        }
        public virtual void Update()
        {
            m_ghost.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetButtonDown("Select"))
            {
                retval = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Destroy(this);
            }
        }
        public override void OnDestroy()
        {
            OnFinished(retval);
        }
    }
}
