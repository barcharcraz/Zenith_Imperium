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
    void Start()
    {
        
        //find something with a terrain on it, this is a best guess
        //of what the user may want the camera to be looking at
        Terrain terrain = UnityEngine.Object.FindObjectsOfType(typeof(Terrain))[0] as Terrain;
		
        
        //if the user has selected a rendertexture in the inspector to be the target
        //then use that one, otherwise make a new rendertexture and make it avalible
        //via the TargetTexture field
        if (TargetTexture != null)
        {
            m_minimap = new Minimap(TargetTexture, terrain.collider.bounds);
        } else {
            m_minimap = new Minimap(terrain.collider.bounds);
            TargetTexture = m_minimap.Image;
        }
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
}

