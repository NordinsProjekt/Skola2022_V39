using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class NumberSeries
    {
        private Dictionary<double, int> list = new Dictionary<double, int>();

        public void Add(double index, int count)
        {
            try
            {
                list.Add(index, count);
            }
            catch (Exception)
            {

            }
        }

    }
}
