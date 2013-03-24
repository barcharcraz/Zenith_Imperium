using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Commands;

namespace Interface
{
    class UnityCommandQueueUI : ICommandQueueUI
    {
        public void drawCommandQueue(Queue<Type> toDraw)
        {
            /*GUILayout.BeginVertical();
            foreach (ICommandBase c in toDraw)
            {
                GUILayout.Label(c.ToString());
            }
            GUILayout.EndVertical();*/
        }
    }
}
