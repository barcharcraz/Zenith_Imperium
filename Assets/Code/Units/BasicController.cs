using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Units;
using Commands;


public abstract class BasicController : MonoBehaviour
{
    private IUnitInfo m_info;
    public IUnitInfo Info
    {
        get { return m_info; }
        set { m_info = value; }
    }
    public Player Owner { get; set; }
    private GameObject m_selectionBox;
    public CommandManager CommandQueue { get; set; }

    public virtual void OnIssueCommand(Vector3 pos) { }

    void Start()
    {

        CommandQueue = new CommandManager(this.gameObject);
        initSelectionBox();
    }
    public virtual void OnDeselect()
    {
        m_selectionBox.SetActive(false);
    }
    public virtual void OnSelect()
    {


        m_selectionBox.SetActive(true);

        //m_selectionBox.transform.parent = transform;
        //m_selectionBox.transform.position = collider.bounds.center - new Vector3(0, collider.bounds.extents.y, 0);

    }
    protected virtual void initSelectionBox()
    {
        Mesh boxMesh = new Mesh();
        m_selectionBox = new GameObject("SelectionBox");
        m_selectionBox.AddComponent<MeshFilter>();
        m_selectionBox.AddComponent<MeshRenderer>();


        float x = collider.bounds.extents.x;
        float y = 0;
        float z = collider.bounds.extents.z;
        Vector3[] verts = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] tris = { 0, 1, 2, 2, 1, 3 };
        //prepare to be annoyed by delclaring the square the hard way(tm)
        verts[0] = new Vector3(-x, y, -z);
        verts[1] = new Vector3(-x, y, z);
        verts[2] = new Vector3(x, y, -z);
        verts[3] = new Vector3(x, y, z);
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(1, 1);

        boxMesh = new Mesh();
        boxMesh.vertices = verts;
        boxMesh.triangles = tris;
        boxMesh.uv = uv;
        boxMesh.RecalculateNormals();

        m_selectionBox.GetComponent<MeshFilter>().mesh = boxMesh;
        m_selectionBox.transform.position = transform.position;
        m_selectionBox.transform.parent = transform;
        m_selectionBox.SetActive(false);

    }

}

