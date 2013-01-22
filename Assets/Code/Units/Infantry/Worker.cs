using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units.Infantry
{
    class Worker : PeonInfo
    {
        public Worker()
        {
            Name = "Worker";
            Cost.Food = 50;
            HarvestRate = 0.1f;
            harvestAmount = 50;
            Prefab = UnityEngine.Resources.Load("Units/Worker") as UnityEngine.GameObject;
            UnitCommands.Add(new Commands.Move());
            UnitCommands.Add(new Commands.Harvest());
            UnitCommands.Add(new Commands.Return());
        }
    }
}
