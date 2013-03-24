using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using System.Text;
using Units.Buildings;

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
            UnitCommands.Add(typeof(Commands.Move));
            UnitCommands.Add(new CommandBuilder<Harvest>());
            UnitCommands.Add(new CommandBuilder<Return>());
            UnitCommands.Add(new CommandBuilder<Build<TownCenter>>());
			UnitCommands.Add(new CommandBuilder<Build<Barracks>>());
        }
    }
}
