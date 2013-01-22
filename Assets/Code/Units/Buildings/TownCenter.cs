using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Units.Buildings
{
    class TownCenter : UnitInfo, IResourceDrop
    {
        public TownCenter()
        {
            Name = "Campfire";
            Cost.Food = 1000;
            Cost.Stone = 500;
            Prefab = UnityEngine.Resources.Load("Buildings/prim_TownCenter") as UnityEngine.GameObject;
            UnitCommands.Add(new Commands.ProduceUnit(new Units.Infantry.Warrior()));
            UnitCommands.Add(new Commands.ProduceUnit(new Units.Infantry.Worker()));
            Speed = 0;
        }
        
    }
}
