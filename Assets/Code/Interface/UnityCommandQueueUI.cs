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
        public void drawCommandQueue(Queue<Commands.ICommandBase> toDraw)
        {
            GUILayout.BeginVertical();
            foreach (ICommandBase c in toDraw)
            {
                GUILayout.Label(c.Name);
            }
            GUILayout.EndVertical();
        }
    }
}
