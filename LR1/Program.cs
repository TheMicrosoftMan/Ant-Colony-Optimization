using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cities = new string[] { "Akaa", "Alajärvi", "Alavus", "Espoo", "Forssa", "Haapajärvi", "Haapavesi", "Hamina", "Hanko", "Harjavalta", "Haukipudas", "Heinola", "Helsinki", "Huittinen", "Hyvinkää", "Hämeenlinna", "Iisalmi", "Ikaalinen", "Imatra", "Pietarsaari", "Joensuu", "Juankoski", "Jyväskylä", "Jämsä", "Järvenpää", "Kaarina", "Kajaani", "Kalajoki", "Kankaanpää", "Kannus", "Karkkila", "Kaskinen", "Kauhajoki", "Kauhava", "Kauniainen", "Kemi", "Kemijärvi", "Kerava", "Keuruu", "Kitee", "Kiuruvesi", "Kokemäki", "Kokkola", "Kotka", "Kouvola", "Kristiinankaupunki", "Kuhmo", "Kuopio", "Kurikka", "Kuusamo", "Lahti", "Laitila", "Lappeenranta", "Lapua", "Lieksa", "Lohja", "Loimaa", "Loviisa", "Maarianhamina", "Mikkeli", "Mänttä-Vilppula", "Naantali", "Nilsiä", "Nivala", "Nokia", "Nurmes", "Uusikaarlepyy", "Närpiö", "Orimattila", "Orivesi", "Oulainen", "Oulu", "Outokumpu", "Paimio", "Parainen", "Parkano", "Pieksämäki", "Pori", "Porvoo", "Pudasjärvi", "Pyhäjärvi", "Raahe", "Raasepori", "Raisio", "Rauma", "Riihimäki", "Rovaniemi", "Saarijärvi", "Salo", "Sastamala", "Savonlinna", "Seinäjoki", "Siuntio", "Somero", "Suonenjoki", "Tampere", "Tornio", "Turku", "Ulvila", "Uusikaupunki", "Vaasa", "Valkeakoski", "Vantaa", "Varkaus", "Viitasaari", "Virrat", "Ylivieska", "Ylöjärvi", "Ähtäri", "Äänekoski" };
            List<CitiesPair> pairs = new List<CitiesPair>();

            for (int i = 0; i < 25/*cities.Length*/; i++)
            {
                for (int j = 0; j < cities.Length; j++)
                {
                    string url = "https://www.rasstoyanie.com/route.json?stops=" + cities[i] + "|" + cities[j];
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    var webResponse = request.GetResponse();
                    var webStream = webResponse.GetResponseStream();
                    var responseReader = new StreamReader(webStream);
                    var response = responseReader.ReadToEnd();
                    CitiesPair temp = JsonConvert.DeserializeObject<CitiesPair>(response);
                    responseReader.Close();

                    pairs.Add(new CitiesPair(cities[i], cities[j], temp.distance));
                    Console.WriteLine("j = " + j + "\\" + cities.Length + "\ni =" + i + "\\" + /*cities.Length*/25 + "\n");
                }
            }

            
            for (int i = 0; i < pairs.Count; i++)
            {
                Console.WriteLine("From: " + pairs[i].from + "\nTo: " + pairs[i].to + "\nDistance: " + pairs[i].distance + "\n\n");
            }

            string json = JsonConvert.SerializeObject(pairs.ToArray());
            File.WriteAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".json", json);

            Console.ReadLine();
        }
    }
}
