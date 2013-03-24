using System;
using UnityEngine;
using System.Collections.Generic;
using Commands;


namespace Units
{
    
    public class UnitInfo : IUnitInfo
    {
        public string Name { get; set; }
        public List<Type> UnitCommands
        {
            get { return m_unitCommands; }
            set { m_unitCommands = value; }
        }
        public float constructionTime
        {
            get { return Cost.Sum() / 5; }
        }
        public Resources Cost;
        public virtual GameObject Prefab { get; set; }
        public List<Type> m_unitCommands;
        public float Speed;
        public virtual float MaxHealth { get; set; }
        public virtual float CurrHealth { get; set; }
        public UnitInfo()
        {
            Cost = new Resources();
            UnitCommands = new List<Type>();
            Speed = 0;
        }
        public GameObject CreateUnit(Player owner, Vector3 pos, Quaternion rotation, bool active=true)
        {
            // set cur health to max health seems reasonable, note that the unit spec should specify the
            // max health
            CurrHealth = MaxHealth;

            GameObject retval;
            retval = UnityEngine.Object.Instantiate(Prefab, pos, rotation) as GameObject;
            retval.SetActive(active);
            retval.GetComponent<BasicController>().Info = this;
            retval.GetComponent<BasicController>().Owner = owner;
            owner.HarvestedResources -= Cost;
            
            return retval;
        }
        public GameObject CreateGhost(Vector3 pos, Quaternion rotation, bool active=true)
        {
            // we don't need to initialize the scripts since we are going to kill
            // em off anyways
            GameObject retval = UnityEngine.Object.Instantiate(Prefab, pos, rotation) as GameObject;
            retval.SetActive(active);
            Component[] comp = retval.GetComponentsInChildren<Component>(true);
            //we want to disable everything that is not rendering related
            //since we are not going to actually interact with this object we can just delete
            //everything that we dont want
            foreach (Component c in comp)
            {
                if (!(c is MeshFilter) && !(c is MeshRenderer) && !(c is Transform))
                {
                    UnityEngine.Object.Destroy(c);
                }
            }
            
            MeshRenderer[] rend = retval.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in rend)
            {
                //make the object translucent
                Color temp = r.material.color;
                temp.a = 0.25f;
                r.material.color = temp;
            }
            return retval;
        }
        public virtual GameObject CreateUnit(Player owner, Vector3 pos)
        {
            return CreateUnit(owner, pos, Quaternion.identity);
        }
    }
}