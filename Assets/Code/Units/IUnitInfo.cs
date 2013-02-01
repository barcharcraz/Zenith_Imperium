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
        List<ICommandBase> UnitCommands { get; set; }
        GameObject CreateUnit(Player owner, Vector3 pos, Quaternion rotation);
    }
}
