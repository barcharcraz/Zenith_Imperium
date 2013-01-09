using UnityEngine;
using System.Collections;
using System;



public class Minimap {
    private const int DEF_WIDTH = 128;
    private const int DEF_HEIGHT = 128;
    private const int DEF_DEPTH = 24;
    private RenderTexture m_image;
    
    private GameObject m_overheadObject;
    public RenderTexture Image
    {
        get { return m_image; }
    }
    private Camera m_overhead 
    { 
        get { return m_overheadObject.GetComponent<Camera>(); } 
    }
    public Minimap()
    {
        initalize(new RenderTexture(DEF_WIDTH, DEF_HEIGHT, DEF_DEPTH), new Bounds());
    }
    public Minimap(RenderTexture target, Bounds targetArea)
    {
        initalize(target, targetArea);
    }
    public Minimap(int width, int height, int depth)
    {
        initalize(new RenderTexture(width, height, depth), new Bounds());
    }
    public Minimap(Bounds target)
    {
        initalize(new RenderTexture(DEF_WIDTH, DEF_HEIGHT, DEF_DEPTH), target);
        
        
    }
    private void initalize(RenderTexture target, Bounds targetArea)
    {
        m_image = target;

        m_overheadObject = new GameObject("MinimapCamera", typeof(Camera));
        //rotate the camera so that it is facing down
        m_overhead.transform.Rotate(m_overhead.transform.right, 90);
        m_overhead.targetTexture = m_image;
        m_overhead.aspect = 1;
        m_overhead.orthographic = true;
        m_overhead.transform.position = targetArea.center;
        m_overhead.orthographicSize = getMaxComponent(targetArea.extents);
    }
    /// <summary>
    /// Gets a Mesh that covers the plane where the viewing frustum
    /// of the playerView camera intersects the provided terrain
    /// 
    /// Use this to construct minimaps that show where
    /// the player is viewing
    /// </summary>
    /// <param name="terrain">the terrain the player is looking at</param>
    /// <param name="playerView">the camera the player is looking through</param>
    /// <returns></returns>
    public Mesh getViewBoxMesh(Terrain terrain, Camera playerView)
    {
        Ray[] rays = new Ray[4]; //each element of the array should be a corner of the projected rectangle
        RaycastHit[] hits = new RaycastHit[4];
        Vector3[] points = new Vector3[4];
        rays[0] = playerView.ViewportPointToRay(new Vector3(0, 0, 0));
        rays[1] = playerView.ViewportPointToRay(new Vector3(1, 0, 0));
        rays[2] = playerView.ViewportPointToRay(new Vector3(1, 1, 0));
        rays[3] = playerView.ViewportPointToRay(new Vector3(0, 1, 0));
        int[] tris = { 0, 3, 2, 2, 1, 0 };
        for (int i = 0; i < rays.Length; i++)
        {
            terrain.collider.Raycast(rays[i], out hits[i], float.PositiveInfinity);
        }
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                points[i] = hits[i].point;
            }
            else
            {
                points[i] += terrain.collider.ClosestPointOnBounds(rays[i].GetPoint(float.PositiveInfinity));
            }

            

        }
        Mesh box = new Mesh();
        box.vertices = points;
        box.triangles = tris;
        box.uv = new Vector2[4];
        box.RecalculateNormals();
        return box;
    }
    /// <summary>
    /// same function as <see cref="Minimap.GetViewBoxMesh" /> but returns a meshFilter containing the mesh
    /// </summary>
    /// <param name="terrain">the terrain the player is looking at</param>
    /// <param name="playerView">the camera the player is looking through</param>
    /// <returns></returns>
    public MeshFilter getViewBoxFilter(Terrain terrain, Camera playerView)
    {
        Mesh box = getViewBoxMesh(terrain, playerView);
        MeshFilter retval = new MeshFilter();
        retval.mesh = box;
        return retval;
    }
    /// <summary>
    /// same function as <see cref="Minimap.getViewBoxMesh"/> and <see cref="Minimap.getViewBoxFilter"/>
    /// but returns a full blown gameobject
    /// </summary>
    /// <param name="terrain">the terrain the player is looking at</param>
    /// <param name="playerView">the camera the player is looking through</param>
    /// <param name="active">sets if the GameObject is initially active and thus
    /// drawn in the scene, false by default</param>
    /// <returns>GameObject representing the intersection of the viewing frustum and <paramref name="terrain"/></returns>
    public GameObject getVewBoxGameObject(Terrain terrain, Camera playerView, bool active = false)
    {
        GameObject retval = new GameObject("MinimapBox");
        retval.SetActive(active);
        retval.GetComponent<MeshFilter>().mesh = getViewBoxMesh(terrain, playerView);
        return retval;
    }

    /// <summary>
    /// gets the max component of a vector
    /// </summary>
    /// <param name="vec">the vector to get data from</param>
    /// <returns>the value of the biggest component of the vector</returns>
    private float getMaxComponent(Vector3 vec)
    {
        float retval = vec.x;
        if (vec.y > retval)
        {
            retval = vec.y;
        }
        if (vec.z > retval)
        {
            retval = vec.z;
        }
        return retval;
    }
    /// <summary>
    /// get the smallest component in Vector3 <paramref name="vec"/>
    /// </summary>
    /// <param name="vec">the vector to calculate the smallest component of</param>
    /// <returns>the value of the smallest component of the vector</returns>
    private float getMinComponent(Vector3 vec)
    {
        float retval = vec.x;
        if (vec.y < retval)
        {
            retval = vec.y;
        }
        if (vec.z < retval)
        {
            retval = vec.z;
        }
        return retval;
    }
}

