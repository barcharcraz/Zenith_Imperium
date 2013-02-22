using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using Navigation;



public class NavigationEditor  : EditorWindow
{
    private Terrain[] m_terrains;
    private GUIContent[] m_terrainNames;
    //TODO: this may need t6o go someplace else
    private HeightField m_heightField;
    [MenuItem ("Window/NavMeshEditor")]
    static void Init()
    {
        NavigationEditor window = (NavigationEditor)EditorWindow.GetWindow(typeof(NavigationEditor));
        
    }
    void OnEnable()
    {
        m_terrains = UnityEngine.Object.FindObjectsOfType(typeof(Terrain)) as Terrain[];
        m_terrainNames = new GUIContent[m_terrains.Length];
        for (int i = 0; i < m_terrainNames.Length; i++)
        {
            m_terrainNames[i] = new GUIContent(m_terrains[i].gameObject.name);
        }
        
    }
    void OnGUI()
    {
        //use the terrain selected in the popup menu, gotta love unityGUI syntax
        Terrain selectedTerrain = m_terrains[EditorGUILayout.Popup(0, m_terrainNames)];
        if (GUILayout.Button("Generate Height Field"))
        {
            m_heightField = generateField(selectedTerrain);
        }
        if (EditorGUILayout.Toggle(false, "Display Height Field"))
        {

        }
    }
    private HeightField generateField(Terrain target)
    {
        Bounds area = target.collider.bounds;
        NavMeshGen gen = new NavMeshGen();
        return gen.getHeightField(10, area);
    }
}

