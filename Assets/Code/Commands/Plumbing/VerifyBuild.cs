using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Commands;
using Units;

namespace Commands.Plumbing
{
    class VerifyBuild<T> : Command where T : IUnitInfo, new()
    {
        private Resources m_playerResoucres;
        private Resources m_cost;
        public VerifyBuild(CommandManager parent)
        {
            m_playerResoucres = parent.GetComponent<BasicController>().Owner.HarvestedResources;
            m_cost = new T().Cost; //heidious I know, thinking of changing unitinfo to a static class

        }
        public void Update()
        {

        }
    }
}
