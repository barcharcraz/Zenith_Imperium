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
		public Build() {} //wierdly I need this for the reflection to work
        public override void Update()
        {
            OnAddCommands(typeof(WaitForBuildPos<T>),typeof(BuildAt<T>));
            OnFinished();
        }
	}
}
