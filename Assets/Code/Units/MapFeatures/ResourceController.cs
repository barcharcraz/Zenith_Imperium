using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Units.MapFeatures;
using System.Text;


public class ResourceController : BasicController
{

    public override ResourceNodeInfo Info { get; set; }
    public ResourceController()
    {
        Info = new ResourceNodeInfo();
    }

    public override void OnIssueCommand(Vector3 pos)
    {
        
    }
}
