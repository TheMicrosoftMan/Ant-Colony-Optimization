using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_Ant_Colony_Optimization
{
    class Path
    {
        public List<CoupleOfCities> coupleOfCitiesList;
        public double distance;

        public Path(List<CoupleOfCities> list, double dist)
        {
            coupleOfCitiesList = list;
            distance = dist;
        }
    }
}
