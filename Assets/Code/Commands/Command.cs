using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
	public class Command : MonoBehaviour
	{
        public event EventHandler OnFinished;
        public virtual void OnDestroy()
        {
            OnFinished(this, EventArgs.Empty);
        }
	}
}
