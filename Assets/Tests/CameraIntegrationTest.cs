using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.IO;
using UnityEngine;



class CameraIntegrationTest : MonoBehaviour
{
    Thread window;
    Minimap map;
    CameraForm form;
    GameObject box;
    void Start()
    {
        map = new Minimap();
        
        form = new CameraForm(new Bitmap(128,128));
        window = new Thread(this.startThread);
        window.Start();
    }
    public void startThread()
    {
        System.Windows.Forms.Application.Run(form);
    }
    void Update()
    {
        if (box == null)
        {
            box = map.getViewBoxGameObject(GetComponent<Camera>(), true, 20.0f);
        }
        else
        {
            map.updateViewBoxGameObject(ref box, GetComponent<Camera>(), 20.0f);
        }
        Texture2D tex = map.Image2D;
        form.Map = new Bitmap(new MemoryStream(tex.EncodeToPNG()));
    }
    
}

class CameraForm : Form 
{
    private volatile Bitmap m_map;
    public Bitmap Map
    {
        get
        {
            return m_map;
        }
        set
        {
            m_map = value;
            this.Invalidate();
            
        }
    }
    public CameraForm(Bitmap texture)
    {
        Map = texture;
        this.Paint += CameraForm_Paint;
    }

    void CameraForm_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.DrawImage(Map, 0, 0);
    }
}
