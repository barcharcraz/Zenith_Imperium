using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    /// <summary>
    /// Simple wrapper around a c# linked list to make the
    /// syntax behave like a dequeue
    /// </summary>
    /// <typeparam name="T">Type of objects to store in the Dequeue</typeparam>
    public class Deque<T> : LinkedList<T>
    {
        public T Dequeue()
        {
            T retval = GetFirstValue();
            RemoveFirst();
            return retval;
        }
        public T Peek()
        {
            return GetFirstValue();
        }
        public void Enqueue(T value)
        {
            AddLast(value);
        }
        public void Push(T value)
        {
            AddFirst(value);
        }
        private T GetFirstValue()
        {
            T retval;
            if (First != null)
            {
                retval = First.Value;
            }
            else
            {
                retval = default(T);
            }
            return retval;
        }
    }
}
