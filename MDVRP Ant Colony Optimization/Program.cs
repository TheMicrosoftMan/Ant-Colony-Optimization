using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_Ant_Colony_Optimization
{
    class Program
    {
        static void Main(string[] args)
        {
            double distance = 0;
            List<Path> paths = new List<Path>(); // всі шляхи, які знайшли комахи

            List<CoupleOfCities> couplesOfCities = new List<CoupleOfCities>(); //пари міст
            couplesOfCities = loadFromJSON();

            Random setPheromone = new Random();
            for (int i = 0; i < couplesOfCities.Count; i++)
            {
                couplesOfCities[i].pheromone = setPheromone.Next(1, 10);
            }

            List<CoupleOfCities> listCites = new List<CoupleOfCities>(); // шлях, який знайшла одна комаха
            List<CoupleOfCities> sameFroms = new List<CoupleOfCities>(); // однакові шляхи, серед яких буде випадково обиратись один

            Random rand = new Random(); // для випадкового вибору одного з декількох маршрутів

            int ants = 10; /*кількість комах*/


            List<Depot> depots = new List<Depot>();
            depots.Add(new Depot("Alavus"));
            depots.Add(new Depot("Karkkila"));
            depots.Add(new Depot("Nivala"));

            for (int depotItem = 0; depotItem < depots.Count; depotItem++)
            {
                string startCityName = depots[depotItem].Name;
                do
                {
                    Console.WriteLine(ants * 100 / 10 /*кількість комах*/ + "%");
                    CoupleOfCities startCity = new CoupleOfCities();
                    foreach (var item in couplesOfCities)
                    {
                        if (startCityName == item.from)
                        {
                            startCity = item;
                        }
                    }
                    listCites.Add(startCity); // початкове місто
                    string finishCity = startCityName; // кінцеве місто

                    for (int i = 0; i < couplesOfCities.Count;)
                    {
                        string lastCity = listCites[i].to;
                        if (lastCity != finishCity) // якщо не кінцеве місто, то шукаємо гляхи
                        {
                            foreach (var item in couplesOfCities) // з пар міст знаходемо те, в яке можна перейти
                            {
                                if (lastCity == item.from) // якщо є декілька міст, в які можна перейти, то заносимос їх...
                                {
                                    sameFroms.Add(item); // ... в  цей список
                                }
                            }

                            if (sameFroms.Count == 0) // якщо тупікове місто, то очищуємо списки міст і починаємо пошук з початку
                            {
                                listCites.Clear();
                                sameFroms.Clear();
                                listCites.Add(couplesOfCities[0]);
                                i = 0;
                            }
                            else
                            {
                                double sum = 0;
                                foreach (var item in sameFroms)
                                {
                                    if (item.distance != 0)
                                    {
                                        sum += item.pheromone / item.distance;
                                    }
                                }
                                for (int e = 0; e < sameFroms.Count; e++)
                                {
                                    sameFroms[e].imovir = 100 * (1 / (sameFroms[e].distance * sameFroms[e].pheromone) / (sum));
                                }
                                listCites.Add(sameFroms[rand.Next(sameFroms.Count)]); // вибираємо випадкове місто зі списку міст, в які можна перейти, та додаємо його в список міст
                                sameFroms.Clear();
                                i++;
                            }
                        }
                        else // якщо досягнуто остаточного міста, то закінчуємо пошук
                        {
                            break;
                        }
                    }

                    distance = sumDistance(listCites); // обраховуємо загальну відстань знайденого шляху

                    paths.Add(new Path(new List<CoupleOfCities>(listCites), distance)); // додаємо шлях до всіх шляхів, які знайшли комахи

                    listCites.Clear(); // очищуємо шлях

                    ants--; // переходимо до найступної комахи
                } while (ants > 0);

                // визначення найкоротшого шлях з усіх знайдених комахами шляхів
                depots[depotItem].Path = paths[0];
                foreach (var item in paths)
                {
                    if (item.distance > depots[depotItem].Path.distance)
                    {
                        depots[depotItem].Path = item;
                    }
                    else
                    {
                        continue;
                    }
                }

                write(depots[depotItem], depots[depotItem].Path.coupleOfCitiesList, depots[depotItem].Path.distance); // виводимо цей шлях і його дистанцію

                for (int r = 0; r < depots[depotItem].Path.coupleOfCitiesList.Count; r++)
                {
                    for (int t = 0; t < couplesOfCities.Count; t++)
                    {
                        if (depots[depotItem].Path.coupleOfCitiesList[r].from == couplesOfCities[t].from && depots[depotItem].Path.coupleOfCitiesList[r].to == couplesOfCities[t].to)
                        {
                            couplesOfCities.Remove(couplesOfCities[t]);
                        }
                    }
                }

                paths.Clear();
                ants = 10;
            }

            Console.ReadLine();
        }

        public static double sumDistance(List<CoupleOfCities> cities)
        {
            double distance = 0;
            for (int i = 0; i < cities.Count; i++)
            {
                distance += cities[i].distance;
            }
            return distance;
        }

        public static List<CoupleOfCities> loadFromJSON()
        {
            string file = File.ReadAllText(@"LR1_formated.json");
            List<CoupleOfCities> couplesOfCities = JsonConvert.DeserializeObject<List<CoupleOfCities>>(file);
            return couplesOfCities;
        }

        public static void write(Depot depot, List<CoupleOfCities> listCites, double distance)
        {
            Console.Write("Path from depot " + depot.Name + ": ");
            for (int i = 0; i < listCites.Count; i++)
            {
                Console.Write(listCites[i].from + " - " + listCites[i].to + " - ");
            }
            Console.WriteLine("\nDistance: " + distance + " miles");
        }
    }
}
