using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units.Infantry
{
    class Worker : UnitInfo
    {
        public Worker()
        {
            Name = "Worker";
            Cost.Food = 50;
            Prefab = UnityEngine.Resources.Load("Units/Worker") as UnityEngine.GameObject;
            UnitCommands.Add(new Commands.Move());
        }
    }
}
