using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class WaitForClick : Command
    {
        private Vector3 retval;
        public void Update()
        {
            if (Input.GetButtonDown("IssueCommand"))
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
