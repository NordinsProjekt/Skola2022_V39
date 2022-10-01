using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class MyListItem<T> : IItem
    {
        private T value;
        public MyListItem<T> NextItem { get; set; }

        public MyListItem(T value)
        {
            this.value = value;
        }
        public T Value { get { return value; } }

        public object GetValue()
        {
            if (this.Value == null)
                return "Nothing";
            else
                return value;
        }

    }

}
