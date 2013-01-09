using UnityEngine;
using System.Collections.Generic;
using Commands;


namespace Units
{
    public class UnitInfo
    {
        public string Name;
        public Resources Cost;
        public GameObject Prefab;
        public List<ICommand> UnitCommands;
        public float Speed;
        public UnitInfo()
        {
            UnitCommands = new List<ICommand>();
            Speed = 0;
        }
        public virtual GameObject CreateUnit(Player owner, Vector3 pos, Quaternion rotation)
        {
            GameObject retval;
            retval = Object.Instantiate(Prefab, pos, rotation) as GameObject;
            retval.GetComponent<UnitController>().Info = this;
            retval.GetComponent<UnitController>().Owner = owner;

            
            return retval;
        }
        public virtual GameObject CreateUnit(Player owner, Vector3 pos)
        {
            return CreateUnit(owner, pos, Quaternion.identity);
        }
        
    }
}