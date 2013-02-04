using UnityEngine;
using System.Collections;
using System;
using ExtensionMethods;


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
    public Texture2D Image2D
    {
        get { return getTexture2D(Image); }
    }
    public Camera m_overhead 
    { 
        get { return m_overheadObject.GetComponent<Camera>(); } 
    }
    public GameObject OverheadObject
    {
        get { return m_overheadObject; }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Minimap"/> class.
    /// 
    /// This constructor creates a new render texture that, by default is 128x128x24.
    /// In addition this will set the position and view size of the new camera to cover the
    /// whole of the first terrain in the scene. If you want to position the minimap Camera someplace
    /// else use one of the constructors with a bounds argument.
    /// </summary>
    public Minimap()
    {
        initalize(new RenderTexture(DEF_WIDTH, DEF_HEIGHT, DEF_DEPTH), getFirstTerrainBounds());
    }
    public Minimap(RenderTexture texture)
    {
        initalize(texture, getFirstTerrainBounds());
    }
    public Minimap(RenderTexture target, Bounds targetArea)
    {
        initalize(target, targetArea);
    }
    public Minimap(int width, int height, int depth)
    {
        initalize(new RenderTexture(width, height, depth), getFirstTerrainBounds());
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
        m_overhead.clearFlags = CameraClearFlags.SolidColor;
        m_overhead.orthographic = true;
        m_overhead.transform.position = targetArea.center;
        m_overhead.orthographicSize = targetArea.extents.GetMaxComponent();
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
        
        Ray[] rays = new Ray[4];
        float[] hits = new float[4];
        Vector3[] points = new Vector3[4];
        Vector3[] viewPortPoints = new Vector3[4];
        viewPortPoints[0] = new Vector3(0, 0, 0);
        viewPortPoints[1] = new Vector3(1, 0, 0);
        viewPortPoints[2] = new Vector3(1, 1, 0);
        viewPortPoints[3] = new Vector3(0, 1, 0);

        
        int[] tris = { 0, 3, 2, 2, 1, 0 };
        
        for (int i = 0; i < rays.Length; i++)
        {
            rays[i] = playerView.ViewportPointToRay(viewPortPoints[i]);
            Plane p = new Plane(terrain.transform.up, terrain.transform.position);
            p.Raycast(rays[i], out hits[i]);
            if (hits[i] < 0)
            {
                float pHit;

                Ray downRay = new Ray(rays[i].GetPoint(2 * getDiagSize(terrain.collider.bounds.size)), playerView.transform.up * -1);
                p.Raycast(downRay, out pHit);
                points[i] = downRay.GetPoint(pHit);
            } else {
                points[i] = rays[i].GetPoint(hits[i]);
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
    /// Gets a mesh that surrounds the area where the fiewing frustum intersects the terrain.
    /// The width of the sides of this mesh are defined by <paramref name="width"/>.
    /// 
    /// Note that this has twice as many verts as the mesh mesh that simpaly covers the viewing area.
    /// </summary>
    /// <param name="terrain">The terrain the player is looking at</param>
    /// <param name="playerView">the camera the player is looking through</param>
    /// <param name="width">the desired width of the resultant mesh</param>
    /// <returns></returns>
    public Mesh getViewBoxMesh(Terrain terrain, Camera playerView, float width)
    {
        Mesh retval = getViewBoxMesh(terrain, playerView);
        Vector3[] newPoints = new Vector3[8];
        float[] angles = new float[4]; //the angles of the interior of the mesh
        
        //copy the verst of the old viewbox into the start of the new array
        for (int i = 0; i < 4; i++)
        {
            newPoints[i] = retval.vertices[i];
        }
        //claculate all the interior angles
        angles[0] = Vector3.Angle(newPoints[1] - newPoints[0], newPoints[3] - newPoints[0]);
        angles[1] = Vector3.Angle(newPoints[2] - newPoints[1], newPoints[0] - newPoints[1]);
        angles[2] = Vector3.Angle(newPoints[3] - newPoints[2], newPoints[1] - newPoints[2]);
        angles[3] = Vector3.Angle(newPoints[0] - newPoints[3], newPoints[2] - newPoints[3]);
        //convert to exterior angles and divide by two, thus finding
        //the midpoint on the outside
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = 360f - angles[i];
            angles[i] /= 2;
            angles[i]*=-1;
        }
        //calculate the outer points
        for(int i = 0; i<4; i++)
        {
            int im1 = i-1; //i minus 1 with wrapping
            if(im1 < 0)
            {
                im1 = 3;
            }
            int ip1 = i+1; //i plus 1 with wrapping
            if(ip1 > 3)
            {
                ip1 = 0;
            }
            int j = i+4; // the index of the outer vert
            Vector3 a = newPoints[im1] - newPoints[i];// this is the vectore we are rotating from
            Vector3 b = newPoints[ip1] - newPoints[i]; //this is the vector we are rotating toward
            Quaternion q = Quaternion.AngleAxis(angles[i], Vector3.Cross(a, b));
            newPoints[j] = q * a.normalized;
            newPoints[j] *= width;
            newPoints[j] += newPoints[i];
        }
        
        
        int[] newTris = {0, 1, 5, 5, 4, 0, 1, 2, 6, 6, 5, 1, 2, 3, 7, 7, 6, 2, 3, 0, 4, 4, 7, 3};
        retval.vertices = newPoints;
        retval.triangles = newTris;
        
        retval.RecalculateNormals();
        return retval;
        

        
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
    /// <param name="width">the width of the outline, if set to zero the function will return a plane covering
    /// the entire intersection with the terrain</param>
    /// <returns>GameObject representing the intersection of the viewing frustum and <paramref name="terrain"/></returns>
    public GameObject getViewBoxGameObject(Terrain terrain, Camera playerView, bool active = false, float width = 1.0f)
    {
        GameObject retval = new GameObject("MinimapBox");
        
        retval.SetActive(active);
        //translate up a tad to stop z fighting
        //TODO: use a custom shader instead
        //retval.transform.Translate(0.0f, 0.1f, 0.0f);
        retval.AddComponent<MeshFilter>();
        if (width == 0)
        {
            retval.GetComponent<MeshFilter>().mesh = getViewBoxMesh(terrain, playerView);
        }
        else
        {
            retval.GetComponent<MeshFilter>().mesh = getViewBoxMesh(terrain, playerView, width);
        }
        retval.AddComponent<MeshRenderer>();
        return retval;
    }
    /// <summary>
    /// Gets a view box game object of <paramref name="width"/> using 
    /// the first terrain in the scene as the one of get a view box on.
    /// </summary>
    /// <param name="playerView">the camera that the player is looking through</param>
    /// <param name="active">if true will display the gameobject immediately</param>
    /// <param name="width">the width of the edges of the view box</param>
    /// <returns>a GameObject representing an outline around where the player's view
    /// intersects with the terrain</returns>
    public GameObject getViewBoxGameObject(Camera playerView, bool active = false, float width = 1.0f)
    {
        return getViewBoxGameObject(getFirstTerrain(), playerView, active, width);
    }
    public void updateViewBoxGameObject(ref GameObject gobj, Terrain terrain, Camera playerView, float width = 1.0f)
    {
        gobj.GetComponent<MeshFilter>().mesh = getViewBoxMesh(terrain, playerView, width);
    }
    public void updateViewBoxGameObject(ref GameObject gobj, Camera playerView, float width = 1.0f)
    {
        gobj.GetComponent<MeshFilter>().mesh = getViewBoxMesh(getFirstTerrain(), playerView, width);
    }
    
    
    
    private Bounds getFirstTerrainBounds()
    {
        Bounds retval;
        Component terrain = UnityEngine.Object.FindObjectOfType(typeof(Terrain)) as Component;
        retval = terrain.collider.bounds;
        return retval;
    }
    private Terrain getFirstTerrain()
    {
        Terrain retval;
        retval = UnityEngine.Object.FindObjectOfType(typeof(Terrain)) as Terrain;
        return retval;
    }
    private Texture2D getTexture2D(RenderTexture src)
    {
        Texture2D retval = new Texture2D(src.width, src.height, TextureFormat.RGB24, false);
        //make sure we can restore the previous active renderTexture
        RenderTexture prvActive = RenderTexture.active;
        RenderTexture.active = src;
        //read the screen pixels of the renderTexture into the new Texture2D
        retval.ReadPixels(new Rect(0,0,src.width, src.height),0,0);
        RenderTexture.active = prvActive;
        retval.Apply();
        return retval;
    }
    private float getDiagSize(Vector3 vec)
    {
        double retval = 0;
        double x = (double)vec.x;
        double y = (double)vec.y;
        double z = (double)vec.z;
        retval = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        return (float)retval; 
    }
}

