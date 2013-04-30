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
        private CommandManager m_parent;
        public Build(CommandManager parent)
        {
            m_parent = parent;
        }
        public override void Update()
        {
            Units.Resources parentResources = m_parent.GetComponent<BasicController>().Owner.HarvestedResources;
            Units.Resources cost = new T().Cost;
            if (parentResources.HasEnoughResources(cost))
            {
                OnAddCommands(typeof(WaitForBuildPos<T>), typeof(MoveTo), typeof(BuildAt<T>));
            }
            OnFinished();
        }
    }
}
