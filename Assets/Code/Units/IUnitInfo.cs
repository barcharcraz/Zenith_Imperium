using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;
using UnityEngine;

namespace Units
{
    public interface IUnitInfo
    {
        string Name { get; set; }
        List<Type> UnitCommands { get; set; }
        GameObject CreateUnit(Player owner, Vector3 pos, Quaternion rotation, bool active = true);
        GameObject CreateGhost(Vector3 pos, Quaternion rotation, bool active = true);
        float constructionTime { get; }
        Resources Cost { get; }
        float MaxHealth { get; set; }
        float CurrHealth { get; set; }
        float AttackRange { get; set; }
        float AttackPower { get; set; }
        
    }
}
