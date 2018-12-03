using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_Ant_Colony_Optimization
{
    class Depot
    {
        public string Name { get; set; }
        public Path Path { get; set; }

        public Depot(string name)
        {
            Name = name;
        }
    }
}
