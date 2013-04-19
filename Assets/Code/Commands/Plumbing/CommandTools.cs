using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;

namespace Commands.Plumbing
{
	static class CommandTools
	{
        /// <summary>
        /// check if the unit that owns <paramref name="manager"/> has enough resources
        /// to build the unit detailed in info
        /// </summary>
        /// <param name="manager">the manager to check aganst</param>
        /// <param name="info">the unit to check</param>
        /// <returns>true if the player can build info, false otherwise</returns>
        public static bool checkResources(CommandManager manager, IUnitInfo info)
        {
            Resources cost = info.Cost;
            Resources harvested = manager.GetComponent<BasicController>().Owner.HarvestedResources;
            return harvested.HasEnoughResources(cost);
        }
	}
}
