using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed record RecordEntry(string Firstname,string Lastname,string Town,string PhoneNumber)
    {
        public override string ToString()
        {
            return $"{Firstname} {Lastname} | {Town} | {PhoneNumber}";
        }
    }
}
