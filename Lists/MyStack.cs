using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class MyStack
    {
        private string[] arr = new string[0];

        public void Pop()
        {
            //Ta bort sista elementet i arrayen
            int size = arr.Length;
            if (size <= 0) return;
            string[] temp = new string[size-1];

            for (int i = 0; i < arr.Length-1; i++)
            {
                temp[i] = arr[i];
            }
            arr = temp;
        }
        public void Push(string item)
        {
            //Lägg till ett element sist i arrayen
            int size = arr.Length;
            string[] temp = new string[size+1];
            for (int i = 0; i < arr.Length; i++)
            {
                temp[i] = arr[i];
            }
            temp[size] = item;
            arr = temp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.AppendLine(arr[i]);
            }
            return sb.ToString();
        }

    }
}
