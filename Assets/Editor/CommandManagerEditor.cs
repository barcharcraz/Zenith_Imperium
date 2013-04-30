using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Commands;

[CustomEditor(typeof(CommandManager))]
public class CommandManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CommandManager mgr = target as CommandManager;
        EditorGUILayout.BeginVertical();
        foreach (Type T in mgr.QueuedCommands)
        {
            EditorGUILayout.LabelField(T.ToString());
        }
        foreach (Command c in mgr.ExecutingCommands)
        {
            EditorGUILayout.LabelField(c.ToString());
        }
        EditorGUILayout.EndVertical();
    }
}
