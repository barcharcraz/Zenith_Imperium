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
    //TODO: this may need to go someplace else
    private HeightField m_heightField;
    private bool showField = false;
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
        if (m_heightField != null)
        {
            EditorGUILayout.ColorField("Hight Field Generated", Color.green);
        }
        else
        {
            EditorGUILayout.ColorField("Height Field Generated", Color.red);
        }
        if (GUILayout.Button("Generate Height Field"))
        {
            m_heightField = generateField(selectedTerrain);
        }
        
        showField = EditorGUILayout.Toggle("Display Height Field", showField);
        if (showField && m_heightField != null)
        {

            EditorGUI.DrawPreviewTexture(new Rect(0,300,200,200), m_heightField.getBitmap(5));
        }
        GUILayout.BeginVertical();
        if (m_heightField != null)
        {
            EditorGUILayout.LabelField("Height Points: ", m_heightField.Length.ToString());
        }
        GUILayout.EndVertical();
    }
    private HeightField generateField(Terrain target, int resolution = 1)
    {
        Bounds area = target.collider.bounds;
        NavMeshGen gen = new NavMeshGen();
        return gen.getHeightField(resolution, area);
    }
}

