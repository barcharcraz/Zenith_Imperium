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
		public override Type[] ReturnType {
			get {
				return new System.Type[]{typeof(Vector3)};
			}
		}
        public override void Update()
        {
            if (Input.GetButtonDown("Select") && GUIUtility.hotControl == 0)
            {
				Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				Physics.Raycast(screenRay, out hitInfo);
                retval = hitInfo.point;
                OnFinished(retval);
            }
        }
    }
}
