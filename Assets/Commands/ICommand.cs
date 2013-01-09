using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    /// <summary>
    /// This interface defines the concept of a command
    /// a command is something that will appear on a button and or
    /// in a unit's command card
    /// </summary>
    public interface ICommand
    {
        string Name { get; }
        void exec(BasicController controller);
    }
}
