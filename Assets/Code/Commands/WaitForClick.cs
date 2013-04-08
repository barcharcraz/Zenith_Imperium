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
        public override void Update()
        {
            if (Input.GetButtonDown("IssueCommand"))
            {
                retval = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                OnFinished(retval);
            }
        }
    }
}
