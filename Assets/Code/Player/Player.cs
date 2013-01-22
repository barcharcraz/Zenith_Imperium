using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using Units;
using Units.Buildings;
using Commands;
using Events;

public class Player : MonoBehaviour
{
    public event ClickEventHandler SendCommand;
    private BasicController m_selectedUnit;
    public Camera playerView;
    private Minimap minimap;
    private GameObject viewBox;
    public BasicController SelectedUnit
    {
        get
        {
            return m_selectedUnit;
        }
        set
        {
            if (m_selectedUnit != null)
            {
                m_selectedUnit.OnDeselect();
            }
            m_selectedUnit = value;
            m_selectedUnit.OnSelect();
        }
    }
    public Resources HarvestedResources { get; set; }
    public UnityEngine.Vector3 startPos;
    void Start()
    {
        HarvestedResources = new Resources();
        if (playerView == null)
        {
            playerView = GetComponent<Camera>();
        }
        minimap = new Minimap();
        TownCenter center = new TownCenter();
        center.CreateUnit(this, startPos, Quaternion.identity);
    }
    void Update()
    {
        if (viewBox == null)
        {
            viewBox = minimap.getViewBoxGameObject(playerView, true, 30.0f);
        }
        else
        {
            minimap.updateViewBoxGameObject(ref viewBox, playerView, 30.0f);
        }
        if (Input.GetButtonDown("IssueCommand"))
        {
            if (SelectedUnit != null)
            {
                RaycastHit hit = new RaycastHit();
				Physics.Raycast(playerView.ScreenPointToRay(Input.mousePosition), out hit);
                SelectedUnit.OnIssueCommand(hit.point);
            }
        }
        if (Input.GetButtonDown("Select"))
        {
            if (SendCommand != null)
            {
                SendCommand(this, new ClickEventArgs(Input.mousePosition, playerView));
            }
        }
    }
    void OnGUI()
    {
        if (SelectedUnit != null)
        {
            
            foreach (ICommand c in SelectedUnit.Info.UnitCommands)
            {
                if (GUILayout.Button(c.Name))
                {
                    c.exec(SelectedUnit);
                }
            }
        }
        
        GUI.DrawTexture(new Rect(128, 0, 128, 128), minimap.Image, ScaleMode.StretchToFill, false);
        GUILayout.BeginVertical();
        
        GUILayout.Label("Food: " + HarvestedResources.Food);
        GUILayout.Label("Gold: " + HarvestedResources.Gold);
        GUILayout.Label("Stone: " + HarvestedResources.Stone);
        GUILayout.Label("Tin: " + HarvestedResources.Tin);
        GUILayout.Label("Copper: " + HarvestedResources.Copper);
        GUILayout.Label("Bronze: " + HarvestedResources.Bronze);
        GUILayout.EndVertical();
    }

}
