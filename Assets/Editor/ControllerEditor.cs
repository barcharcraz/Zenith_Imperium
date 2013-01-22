using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Text;
using Units;
using Units.Infantry;
using Units.MapFeatures;



public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        UnitInfo info = (target as BasicController).Info;
        if (info is ResourceNodeInfo)
        {
            ResourceNodeInfo node = info as ResourceNodeInfo;
            showResources(node.CurrentResources);
            
        }
        if (info is PeonInfo)
        {
            PeonInfo peon = info as PeonInfo;
            showResources(peon.StoredResources);

        }
        



    }
    private void showResources(Resources toShow)
    {
        for (int i = 0; i < toShow.ResourceArray.Length; i++)
        {
            toShow.ResourceArray[i] = EditorGUILayout.IntField(Enum.GetName(typeof(Resources.ResourceTypes), i),
                                     toShow.ResourceArray[i]);
        }
    }

}
