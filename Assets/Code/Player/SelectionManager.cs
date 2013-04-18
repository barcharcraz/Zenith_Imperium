using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ExtensionMethods;
using UnityEngine;

class SelectionManager
{
    private Player m_player;
    private Vector3 m_initialMouse;
    public SelectionManager(Player parent)
    {
        m_player = parent;
    }
    public void HandleInput()
    {
        BasicController unitClicked = OverUnit();
        //if we moused down on a unit go ahead and select it
        if (Input.GetButtonDown("Select") && unitClicked != null)
        {
            m_player.SelectedUnits = new List<BasicController>() { unitClicked };
        }
        if (Input.GetButtonDown("Select"))
        {
            m_initialMouse = Input.mousePosition;
        }
        if (Input.GetButton("Select") && (Input.mousePosition - m_initialMouse).magnitude > 0.5)
        {
            
            Rect selectionBox = new Rect(
                m_initialMouse.x,
                Screen.height - m_initialMouse.y,
                Input.mousePosition.x - m_initialMouse.x,
                m_initialMouse.y - Input.mousePosition.y);
            GUI.Box(selectionBox, new GUIContent());
        }
        if (Input.GetButtonUp("Select") && (Input.mousePosition - m_initialMouse).magnitude > 0.5)
        {
            Ray[] selectionWorldPos = new Ray[2];
            float[] distances = new float[2];
            selectionWorldPos[0] = m_player.playerView.ScreenPointToRay(m_initialMouse);
            selectionWorldPos[1] = m_player.playerView.ScreenPointToRay(Input.mousePosition);
            Terrain t = GameObject.FindObjectOfType(typeof(Terrain)) as Terrain;
            Plane p = new Plane(t.transform.up, t.transform.position);
            p.Raycast(selectionWorldPos[0], out distances[0]);
            p.Raycast(selectionWorldPos[1], out distances[1]);
            Vector3 startPos = selectionWorldPos[0].GetPoint(distances[0]);
            Vector3 endPos = selectionWorldPos[1].GetPoint(distances[1]);
            BasicController[] controllers = GameObject.FindObjectsOfType(typeof(BasicController)) as BasicController[];
            IEnumerable<BasicController> selected = 
                from cont
                in controllers
                where cont.transform.position.IsStrictlyGreaterThan(startPos) && 
                      cont.transform.position.IsStrictlyLessThan(endPos)
                select cont;
            m_player.SelectedUnits = selected.ToList();
        }
    }
    /// <summary>
    /// tests if the mouse is over a unit and returns true if it is
    /// </summary>
    /// <param name="cont">the unit that the mosue was over, this is an out param</param>
    /// <returns>true if the mouse was over a unit false otherwise</returns>
    private bool isOverUnit(out BasicController cont)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        BasicController retval = null;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            retval = hitInfo.collider.gameObject.GetComponent<BasicController>();
        }
        //will return null if we did not hit anything
        if (retval != null)
        {
            cont = retval;
            return true;
        }
        else
        {
            cont = null;
            return false;
        }
    }
    private bool isOverUnit()
    {
        //we are throwing this away hence the name
        BasicController devnull;
        return isOverUnit(out devnull);
    }
    private BasicController OverUnit()
    {
        BasicController retval;
        isOverUnit(out retval);
        return retval;
    }
}
