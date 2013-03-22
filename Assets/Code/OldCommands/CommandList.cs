using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class CommandList
    {
        private List<ICommandBuilderBase> m_supportedCommands;
        public void AddCommand(ICommandBuilderBase builder)
        {
            m_supportedCommands.Add(builder);
        }
    }
}
