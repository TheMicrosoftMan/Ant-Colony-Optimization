using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    class CitiesPair
    {
        public string from, to;
        public int distance { get; set; }

        public CitiesPair(string from, string to, int distance)
        {
            this.from = from;
            this.to = to;
            this.distance = distance;
        }
    }
}
