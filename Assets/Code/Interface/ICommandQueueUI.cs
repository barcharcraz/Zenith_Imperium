using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;

namespace Interface
{
    interface ICommandQueueUI
    {
        void drawCommandQueue(Queue<ICommandBase> toDraw);
    }
}
