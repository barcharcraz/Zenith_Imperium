using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;


public class UnitController : BasicController
{
    
    
    
    public override void OnIssueCommand(Vector3 pos)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = pos;
    }
    
    
}

