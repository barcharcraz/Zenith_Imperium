using System;
using UnityEngine;
using Units;
using UnityEditor;

[CustomEditor(typeof(UnitController))]
public class UnitControllerEditor : ControllerEditor
{
    public override void OnInspectorGUI()
    {
        UnitController cont = target as UnitController;
        if (cont.Info is PeonInfo)
        {
            showResources((cont.Info as PeonInfo).StoredResources);
        }
    }
}
