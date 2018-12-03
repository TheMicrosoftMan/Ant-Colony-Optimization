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
        public double distance;
        public double pheromone { get; set; }
        public double imovir { get; set; }

        public CoupleOfCities(string f, string t, double d)
        {
            from = f;
            to = t;
            distance = d;
        }

        public CoupleOfCities()
        {
        }
    }
}
