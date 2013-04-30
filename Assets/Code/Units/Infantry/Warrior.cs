using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Units;
using UnityEngine;

namespace Units.Infantry
{
    class Warrior : UnitInfo
    {
        public Warrior()
        {
            Name = "Warrior";
            Cost.Food = 10;
            Cost.Stone = 10;
            Prefab = UnityEngine.Resources.Load("Units/Warrior") as GameObject;
            Speed = 1;
            MaxHealth = 100;
            AttackRange = 5;
            AttackPower = 1;
            UnitCommands.Add(typeof(Commands.Move));
            UnitCommands.Add(typeof(Commands.Groups.Attack));

        }
    }
}
