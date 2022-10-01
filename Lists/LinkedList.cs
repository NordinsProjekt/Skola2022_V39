using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class LinkedList<T>
    {
        public ListItem<T> FirstElement { get; set; }
        public int Count
        {
            get
            {
                if (FirstElement == null) return 0;
                int count = 1;
                var nextitem = FirstElement.NextItem;
                while (nextitem != null)
                {
                    count++;
                    nextitem = nextitem.NextItem;
                }
                return count;
            }
        }
    }
}
