using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands.Groups
{
    class Build<T> : Command where T : IUnitInfo, new()
    {
        public Build()
        {
            
        }
        public override void Update()
        {
            
            OnAddCommands(typeof(WaitForBuildPos<T>),typeof(MoveTo),typeof(BuildAt<T>));
            OnFinished();
        }
    }
}
