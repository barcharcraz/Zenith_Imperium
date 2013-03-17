using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public interface ICommandBuilderBase
    {
        string Name { get; }
        ICommandBase GetCommand();
    }
    /// <summary>
    /// Has a method that returns a new instance of the command specified in the type paramiter
    /// this is so that each executed command is a totally new instance of the command type and
    /// units are only specifying what commands they can process and not the actuall command
    /// instances
    /// </summary>
    /// <typeparam name="T">the command to build</typeparam>
    public class CommandBuilder<T> : ICommandBuilderBase where T : ICommandBase, new()
    {
        private string m_name;
        public string Name { get { return m_name; } }
        public CommandBuilder()
        {
            //we need to instantiate the object to get its name
            //but we can throw it out after that. Non-ideal but there
            //is nothing we can do
            T temp = new T();
            
            m_name = temp.Name;
        }
        public T GetCommand()
        {
            return new T();
        }
        ICommandBase ICommandBuilderBase.GetCommand()
        {
            return GetCommand() as ICommandBase;
        }
    }
}
