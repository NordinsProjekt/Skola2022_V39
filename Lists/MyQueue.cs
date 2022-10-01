using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class MyQueue<T>
    {
        private System.Collections.Generic.LinkedList<T> queue = new System.Collections.Generic.LinkedList<T>();
        private int count = 0;

        /// <summary>
        /// Takes out the first element from the list.
        /// </summary>
        public T Next
        {
            get
            {
                if (count == 0) return default(T);
                T temp = queue.First.Value;
                queue.RemoveFirst();
                count--;
                return temp;
            }
        }
        /// <summary>
        /// Adds value to the last place in the q;
        /// </summary>
        public void Add(T item)
        {
            if (queue.Count == 0)
                queue.AddFirst(item);
            else
                queue.AddLast(item);
            count++;
        }

        public void PrintIt()
        {
            foreach (IItem item in queue)
            {
                Console.WriteLine(item.GetValue());
            }
        }
    }
    public sealed class MyQueue
    {
        private System.Collections.Generic.LinkedList<object> queue = new System.Collections.Generic.LinkedList<object>();
        private int count = 0;

        /// <summary>
        /// Takes out the first element from the list.
        /// </summary>
        public object Next
        {
            get
            {
                if (count == 0) return new object();
                object temp = queue.First.Value;
                queue.RemoveFirst();
                count--;
                return temp;
            }
        }
        /// <summary>
        /// Adds value to the last place in the q;
        /// </summary>
        public void Add(object item)
        {
            if (queue.Count == 0)
                queue.AddFirst(item);
            else
                queue.AddLast(item);
            count++;
        }

        public void PrintIt()
        {
            foreach (var item in queue)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
