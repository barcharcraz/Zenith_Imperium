using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Units.MapFeatures;
using System.Text;

[Serializable]
public class ResourceController : BasicController
{
    [SerializeField]
    private ResourceNodeInfo m_info;
    public ResourceNodeInfo Info
    {
        get { return m_info; }
        set { m_info = value; }
    }
    public ResourceController()
    {
        if (Info == null)
        {
            Info = new ResourceNodeInfo();
        }
    }

    public override void OnIssueCommand(Vector3 pos)
    {
        
    }
}
