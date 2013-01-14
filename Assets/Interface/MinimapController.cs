using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Use this behavior to eaisly create a minimap in your level, Just attach this to
/// a gameobject and it will try and construct an appropriate minimap
/// 
/// If connected to a camera than it can display that camera's view area without you having to specify
/// the camera
/// </summary>
class MinimapController : MonoBehaviour
{
    private Minimap m_minimap;
    public Camera PlayerCamera;
    public RenderTexture TargetTexture;
    private GameObject viewBox;
    void Start()
    {

        m_minimap = new Minimap(GameObject.FindGameObjectWithTag("Player").collider.bounds);
        m_minimap.OverheadObject.camera.orthographicSize = 20;
        //if the user has selected a rendertexture in the inspector to be the target
        //then use that one, otherwise make a new rendertexture and make it avalible
        //via the TargetTexture field
        TargetTexture = m_minimap.Image;
        //only use the current camera if PlayerCamera was not set in the
        //inspector
        if (PlayerCamera == null)
        {
            //check to see if we are connected to a camera
            if (GetComponent<Camera>())
            {
                //and if we are use that camera as the player camera
                PlayerCamera = GetComponent<Camera>();
            }
        }
    }
    void OnGUI()
    {

        GUILayout.BeginVertical();
		GUILayout.Box("",GUILayout.Width(128), GUILayout.Height(128));
		Graphics.DrawTexture(new Rect(0.0f, 0.0f, 128.0f, 128.0f), TargetTexture, new Material(Shader.Find("Unlit/Texture")));
		GUILayout.EndVertical();
		
    }
    void Update()
    {
        m_minimap.OverheadObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 150, 0);
    }
}

