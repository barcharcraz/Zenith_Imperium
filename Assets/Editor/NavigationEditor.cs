using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;



public class NavigationEditor  : EditorWindow
{
    [MenuItem ("Window/NavMeshEditor")]
    static void Init()
    {
        NavigationEditor window = (NavigationEditor)EditorWindow.GetWindow(typeof(NavigationEditor));
    }
}

