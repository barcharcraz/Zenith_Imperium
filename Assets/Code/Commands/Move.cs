using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Events;
namespace Commands
{
    class Move : Command
    {
        public virtual void Update()
        {
            OnAddCommands(typeof(MoveTo), typeof(WaitForClick));
            Destroy(this);
        }
    }
}
