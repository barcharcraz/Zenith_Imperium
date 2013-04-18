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
        private CommandManager m_parent;
        public override Type[] ReturnType
        {
            get
            {
                return new System.Type[] { typeof(Vector3) };
            }
        }
        public BuildAt(CommandManager parent, Vector3 target)
        {
            m_target = target;
            m_parent = parent;
            m_info = new T();
        }
        public override void Update()
        {
            
            m_info.CreateUnit(m_parent.GetComponent<BasicController>().Owner, m_target, Quaternion.identity);
            OnFinished(m_target);
        }
    }
}
