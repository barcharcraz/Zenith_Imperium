using System;
using UnityEngine;
using System.Collections.Generic;
using Commands;


namespace Units
{
    
    public class UnitInfo : IUnitInfo
    {
        public string Name { get; set; }
        public List<ICommandBase> UnitCommands
        {
            get { return m_unitCommands; }
            set { m_unitCommands = value; }
        }
        public Resources Cost;
        public virtual GameObject Prefab { get; set; }
        public List<ICommandBase> m_unitCommands;
        public float Speed;
        public UnitInfo()
        {
            Cost = new Resources();
            UnitCommands = new List<ICommandBase>();
            Speed = 0;
        }
        public GameObject CreateUnit(Player owner, Vector3 pos, Quaternion rotation)
        {
            GameObject retval;
            retval = UnityEngine.Object.Instantiate(Prefab, pos, rotation) as GameObject;
            retval.GetComponent<BasicController>().Info = this;
            retval.GetComponent<BasicController>().Owner = owner;
            owner.HarvestedResources -= Cost;
            
            return retval;
        }
        public virtual GameObject CreateUnit(Player owner, Vector3 pos)
        {
            return CreateUnit(owner, pos, Quaternion.identity);
        }
    }
}