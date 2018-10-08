using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Colony_TSP
{
    class CoupleOfCities
    {
        public string from { get; set; }
        public string to { get; set; }
        public int distance;

        public CoupleOfCities(string f, string t, int d)
        {
            from = f;
            to = t;
            distance = d;
        }
    }
}
