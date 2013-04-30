using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    class GetUnitAt : Command
    {
        private Vector3 m_pos;
        public override Type[] ReturnType
        {
            get
            {
                return new Type[]{typeof(BasicController)};
            }
        }
        public GetUnitAt(Vector3 pos)
        {
            m_pos = pos;
        }
        public override void Update()
        {
            Collider[] hits = Physics.OverlapSphere(m_pos, 1);
            IEnumerable<BasicController> retval = from Collider c in hits
                                                  where
                                                      c.GetComponent<BasicController>() != null
                                                  select c.GetComponent<BasicController>();
            // lets only return the first one
            OnFinished(retval.First());
        }
    }
}
