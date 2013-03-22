using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Commands
{
    public abstract class ComponentTargetedCommand<T, U> : TargetedCommandImp<T, U>
        where T : Component
        where U : BasicController
    {
        protected override T GetTarget(Vector3 clickPos)
        {

            //Find things near where the player clicked
            //1 is small enough that that is likely the thing that the player meant
            //to click on
            Collider[] hits = Physics.OverlapSphere(clickPos, 1);
            foreach (Collider c in hits)
            {
                T retval = c.GetComponent<T>();
                //break as soon as we find something with the right script
                //no need to keep on searching
                if (retval != null)
                {
                    return retval;
                }
            }
            return null;

        }
    }
    public abstract class ComponentTargetedCommand<T, TInterface, U> : TargetedCommandImp<T, U>
        where T : Component
        where U : BasicController
    {
        protected override T GetTarget(Vector3 clickPos)
        {
            Collider[] hits = Physics.OverlapSphere(clickPos, 1);
            foreach (Collider c in hits)
            {
                T retval = (T)c.GetComponent(typeof(TInterface));
                if (retval != null)
                {
                    return retval;
                }
            }
            return null;
        }
    }
}
