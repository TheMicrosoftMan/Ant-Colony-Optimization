using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_Colony_TSP
{
    class Path
    {
        public List<CoupleOfCities> coupleOfCitiesList;
        public int distance;

        public Path(List<CoupleOfCities> list, int dist)
        {
            coupleOfCitiesList = list;
            distance = dist;
        }
    }
}
