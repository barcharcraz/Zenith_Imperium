using System;
using UnityEditor;
using UnityEngine;
using Units.MapFeatures;
using Units.Infantry;
using Units;
[CustomEditor(typeof(ResourceController))]
public class ResourceEditor : ControllerEditor
{

    public override void OnInspectorGUI()
    {
        ResourceController cont = target as ResourceController;
        ResourceNodeInfo info = cont.Info;
        showResources(info.CurrentResources);
    }
}
