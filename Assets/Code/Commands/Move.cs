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
        public override void Update()
        {
            OnAddCommands(typeof(WaitForClick), typeof(MoveTo) );
            OnFinished();
        }
    }
}
