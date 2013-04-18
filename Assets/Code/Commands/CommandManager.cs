﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using Utils;
using ExtensionMethods;

namespace Commands
{
    public class CommandManager : MonoBehaviour
    {
        public Command executingCommand
        {
            get { return m_executionQueue.Peek(); }
            set { m_executionQueue.Push(value); }
        }
        private Dictionary<Type, int> m_commandCount;
        private Queue<Type> m_commandQueue;
        private Deque<Command> m_executionQueue;
        private Dictionary<Type,System.Object> m_argProviders;
        public void Start() 
        {
            m_commandQueue = new Queue<Type>();
            m_executionQueue = new Deque<Command>();
            m_argProviders = new Dictionary<Type, System.Object>();
            m_argProviders.Add(this.GetType(),this);
            m_commandCount = new Dictionary<Type, int>();
        }
        public void handleCommand(Command src, params System.Object[] args)
        {
            //we dont want to be running update while we are in this part of the code
            //not using active since this will change but the end of the block anyways
            Command PrevCommand = m_executionQueue.Dequeue();
            if (src != null)
            {
                src.Finished -= handleCommand;
                src.AddCommands -= AddCommands;
                m_commandCount[src.GetType()]--;
            }
            //we dont want to proceed to the next command if there isnt one
            if(m_commandQueue.Count > 0) {
                Type nextCommand = m_commandQueue.Dequeue();
            
                if (nextCommand.BaseType != typeof(Commands.Command))
                {
                    throw new InvalidOperationException("Commands must inherit from Command type " + nextCommand + "does not");
                }
                IEnumerable<ConstructorInfo> possibleConst = GetCompatibleConstructors(nextCommand, src);
                Command nextCommandinst = InvokeWithProvider(possibleConst.First(), args) as Command;
                //nextCommandinst.parent = this;
                m_executionQueue.Enqueue(nextCommandinst);
            }
            
        }
        public void AddCommands(params Type[] commands)
        {
            foreach (Type c in commands)
            {
                
                AddCommand(c);
            }
        }
        public void AddCommandNow(Command command)
        {
            command.Finished += handleCommand;
            command.AddCommands += AddCommands;
            m_commandCount[command.GetType()]++;
            m_executionQueue.Push(command);
        }
        public void AddCommand(Command command)
        {
            command.Finished += handleCommand;
            command.AddCommands += AddCommands;
            m_commandCount[command.GetType()]++;
            m_executionQueue.Enqueue(command);
        }
        public void AddCommand(Type command)
        {
            m_commandQueue.Enqueue(command);
            m_commandCount[command]++;
            //only want to kick off execution if we are not already executing anything, otherwise that previous thing can continue on its way
            if (executingCommand==null)
            {
                handleCommand(null, null);
            }
            
        }
        public void Update() 
        {
            if(executingCommand != null) 
            {
                executingCommand.Update();
            }
        }

        private IEnumerable<ConstructorInfo> GetCompatibleConstructors(Type command, Command prev)
        {
            //we want to get an empty constructor if there was no prev command
            if(prev == null)
            {
                return GetCompatibleConstructors(command);
            } else {
                return GetCompatibleConstructors(command, prev.ReturnType);
            }
        }
        private IEnumerable<ConstructorInfo> GetCompatibleConstructors(Type command, params Type[] signature)
        {
            /*IEnumerable<ConstructorInfo> retval = from ConstructorInfo c in command.GetConstructors()
                                                  where c.GetParameters().Equals(signature)
                                                  select c; */

            List<ConstructorInfo> retval = new List<ConstructorInfo>();
            //use this so that if we cant find any matches we can still use the void constructor and ignore return values
            //note that this does include the provided args
            ConstructorInfo voidConstructor = null;
            foreach (ConstructorInfo c in command.GetConstructors())
            {
                //we want to split the list into the ordered and unordered parts of the parameter list
                IEnumerable<ParameterInfo> unordered = from ParameterInfo p in c.GetParameters()
                                                where p.Position < c.GetParameters().Length - signature.Length
                                                select p;
                //next we want to ordered part of the parameter list
                IEnumerable<ParameterInfo> ordered = from ParameterInfo p in c.GetParameters()
                                                     where p.Position >= c.GetParameters().Length - signature.Length
                                                     select p;
                //only a match if every element of the unordered section is of a type that is in the parameter providers map
                if (unordered.Count() == 0 || unordered.All((p => m_argProviders.ContainsKey(p.ParameterType))))
                {
                    //if the ordered sections matches
                    if (ordered.ToArray().IsEqual(signature))
                    {
                        retval.Add(c);
                    }
                    else if (ordered.Count() == 0)
                    {
                        //if the ordered section is null than we can still use this if we dont find anything else, so cache it
                        voidConstructor = c;
                    }
                }
            }
            if (retval.Count == 0 && voidConstructor != null)
            {
                retval.Add(voidConstructor);
            }
            return retval;
        }
        private System.Object InvokeWithProvider(ConstructorInfo c, params System.Object[] ordered)
        {
            List<System.Object> providedArgs = new List<object>();
            int orderedLength = ordered == null ? 0 : ordered.Length;
            for (int i = 0; i < c.GetParameters().Length - orderedLength; i++)
            {
                providedArgs.Add(m_argProviders[c.GetParameters()[i].ParameterType]);
            }
            //invoke the constructor with the provided args first followed by the passed in args
            if(ordered != null) 
            {
                return c.Invoke(providedArgs.Concat(ordered).ToArray());
            } 
            else 
            {
                return c.Invoke(providedArgs.ToArray());
            }
            
        }



        public int GetCommandCount(Type command)
        {
            //dont initilize the dictionary until we ask for it
            if (!m_commandCount.ContainsKey(command))
            {
                m_commandCount.Add(command, 0);
            }
            return m_commandCount[command];
        }
        public int GetCommandCount<T>()
        {
            return GetCommandCount(typeof(T));
        }
        
        
    }
}
