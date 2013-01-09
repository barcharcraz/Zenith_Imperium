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
        if (playerView == null)
        {
            playerView = GetComponent<Camera>();
        }
        TownCenter center = new TownCenter();
        center.CreateUnit(this, startPos, Quaternion.identity);
    }
    void Update()
    {
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
                SendCommand(this, new ClickEventArgs(Input.mousePosition));
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
        GUILayout.Label("Food: " + HarvestedResources.Food);
        GUILayout.Label("Gold: " + HarvestedResources.Gold);
        GUILayout.Label("Stone: " + HarvestedResources.Stone);
        GUILayout.Label("Tin: " + HarvestedResources.Tin);
        GUILayout.Label("Copper: " + HarvestedResources.Copper);
        GUILayout.Label("Bronze: " + HarvestedResources.Bronze);
    }

}
