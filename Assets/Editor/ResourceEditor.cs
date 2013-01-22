using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Text;


[CustomEditor(typeof(BasicController))]
public class ResourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BasicController cont = target as BasicController;

        for (int i = 0; i < cont.Info.CurrentResources.ResourceArray.Length; i++)
        {
            cont.Info.CurrentResources.ResourceArray[i] = EditorGUILayout.IntField(Enum.GetName(typeof (Resources.ResourceTypes), i),
                                     cont.Info.CurrentResources.ResourceArray[i]);
        }



    }

}
