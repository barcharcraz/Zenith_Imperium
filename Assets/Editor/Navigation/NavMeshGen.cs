using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Navigation
{
    public class NavMeshGen
    {
        private Collider[] m_statics;
        private LayerMask statics_mask;
        private GameObject[] getStaticObjects() {
            IEnumerable<GameObject> retval = from GameObject g in GameObject.FindObjectsOfType(typeof(GameObject)) 
                                  where GameObjectUtility.GetStaticEditorFlags(g) == StaticEditorFlags.NavigationStatic
                                  select g;
            
            return retval.ToArray();
        }
        private int findTempLayer(int start = 8, int end=31)
        {
            int retval = -1;
            for (int i = start; i <= end; i++)
            {
                if (LayerMask.LayerToName(i) == "")
                {
                    retval = i;
                    break;
                }
            }
            return retval;
        }
        private void addStaticsToLayer(GameObject[] statics, int layer)
        {
            foreach (GameObject g in statics)
            {
                g.layer = layer;
            }
        }
        private void clearLayer(int layer)
        {
            IEnumerable<GameObject> inLayer = from GameObject g in GameObject.FindObjectsOfType(typeof(GameObject))
                                              where g.layer == layer
                                              select g;
            foreach (GameObject g in inLayer)
            {
                //note this clobbers layers, don use layers on statics, will maybe fix later
                g.layer = 0;
            }
        }
        private int setupLayer()
        {
            int tempLayer = findTempLayer();
            addStaticsToLayer(getStaticObjects(), tempLayer);
            return tempLayer;
        }
        
        public HeightField getHeightField(int resolution, Bounds area)
        {
            int tempLayer = setupLayer();
            LayerMask mask = 1 << tempLayer;
            int sizeX = (int)(area.size.x);
            int sizeZ = (int)(area.size.z); //also known as size y but unity is y-up
            HeightField retval = new HeightField(resolution, sizeX, sizeZ);
            for (float x = 0; x < retval.GetLength(0); x+= (1/(float)resolution))
            {
                for (float y = 0; y < retval.GetLength(1); y+=(1/(float)resolution))
                {
                    float maxHeight = area.size.y;
                    RaycastHit info;
                    Ray ray = new Ray(new Vector3((float)x,maxHeight,(float)y), Vector3.down);
                    Physics.Raycast(ray, out info, mask);
                    float height = maxHeight - info.distance;
                    retval.getHeightAtPosition(x,y).height = height;
                }
            }
            return retval;
        }
        //private Mesh generateMesh(HeightField field)
       // {
        //    Mesh retval = new Mesh();
        //    
        //}
        
    }
}
