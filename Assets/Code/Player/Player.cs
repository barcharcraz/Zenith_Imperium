using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using Units;
using Units.Buildings;
using Commands;
using Events;
using Interface;


public class Player : MonoBehaviour
{
    public event MouseEventHandler SendCommand;
    public event MouseEventHandler MouseMove;
    private List<BasicController> m_selectedUnits;
    private SelectionManager m_selectionManager;
    public Camera playerView;
    private Minimap minimap;
    private GameObject viewBox;

    public List<BasicController> SelectedUnits
    {
        get { return m_selectedUnits; }
        set
        {
            if (m_selectedUnits != null)
            {
                foreach (BasicController cont in m_selectedUnits)
                {
                    cont.OnDeselect();
                }
            }
            m_selectedUnits = value;
            foreach (BasicController cont in m_selectedUnits)
            {
                cont.OnSelect();
            }
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   gets or sets the last selected unit </summary>
    ///
    /// <value> the last selected unit </value>
    ///-------------------------------------------------------------------------------------------------
    public BasicController SelectedUnit
    {
        get
        {
            if (SelectedUnits != null && SelectedUnits.Count > 0)
            {
                return m_selectedUnits[m_selectedUnits.Count-1];
            }
            else
            {
                return null;
            }
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
        center.CreateFreeUnit(this, startPos, Quaternion.identity);
        m_selectionManager = new SelectionManager(this);
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

        DispatchCommands();
    }
    
    private void DispatchCommands()
    {
        if (Input.GetButtonDown("IssueCommand"))
        {
            if (SelectedUnits != null)
            {
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(playerView.ScreenPointToRay(Input.mousePosition), out hit);
                foreach (BasicController cont in SelectedUnits)
                {
                    cont.OnIssueCommand(hit.point);
                }
            }
        }
        if (Input.GetButtonDown("Select"))
        {
            // Send command is not null so somebody is waiting for a command target
            // in this case the select button is more like a select target button
            if (SendCommand != null)
            {
                SendCommand(this, new MouseEventArgs(Input.mousePosition, playerView));
            }
            
        }

        // detect mouse movement and fire the correct event
        if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0)
        {
            if (MouseMove != null)
            {
                MouseMove(this, new MouseEventArgs(Input.mousePosition, playerView));
            }
        }
        
    }
    void OnGUI()
    {

        m_selectionManager.HandleInput();
        if (SelectedUnit != null)
        {
            GUILayout.BeginVertical();
            foreach (Type c in SelectedUnit.Info.UnitCommands)
            {
                if (renderCommand(c, SelectedUnit.CommandQueue.GetCommandCount(c).ToString()))
                {
                    //in an ideal world the queue would haave events to handle this
                    //but for now this is eaiser, also the collections with events are all
                    //WPF or .net 4
                    SelectedUnit.CommandQueue.AddCommand(c);
                }
            }
            GUILayout.EndVertical();
            GUILayout.Space(100f);
            GUILayout.Label("Selected Unit Health: " + SelectedUnit.Info.CurrHealth + @"/" + SelectedUnit.Info.MaxHealth);
            //GUILayout.Space(100f);
        }
        GUI.DrawTexture(new Rect(300, 0, 128, 128), minimap.Image, ScaleMode.StretchToFill, false);
        GUILayout.BeginVertical();
        GUILayout.Label("Food: " + HarvestedResources.Food);
        GUILayout.Label("Gold: " + HarvestedResources.Gold);
        GUILayout.Label("Stone: " + HarvestedResources.Stone);
        GUILayout.Label("Tin: " + HarvestedResources.Tin);
        GUILayout.Label("Copper: " + HarvestedResources.Copper);
        GUILayout.Label("Bronze: " + HarvestedResources.Bronze);
        GUILayout.EndVertical();
        
        
        

        
    }
    bool renderCommand(Type command, string additions = "")
    {
        bool retval = false;
        //if we have a timed command we want to draw a label that shows how much time we have left in the building process
        // UNITYGUI sucks mayhaps I will write my own GUI in direct-2d as some point
        /*if (command is ITimedCommand<BasicController>)
        {
            ITimedCommand<BasicController> timed = command as ITimedCommand<BasicController>;
            GUILayout.BeginHorizontal();
            retval = GUILayout.Button(command.Name);
            if (timed.Running)
            {
                GUILayout.Label(timed.RemainingTime.ToString());
            }
            GUILayout.EndHorizontal();
        }
        else
        {*/
            retval = GUILayout.Button(parseCommandButton(command) + additions);
        //}
        return retval;
        
    }

    private string parseCommandButton(Type command)
    {
        String retval;
        retval = command.Name;
        foreach (Type t in command.GetGenericArguments())
        {
            retval += " " + t.Name;
        }
        return retval;
    }

}
