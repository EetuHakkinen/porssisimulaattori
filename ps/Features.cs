using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ps {
    class Features {

        public static Load Loadings(Stocklist stocks) {
            Userlist u = new Userlist();
            u.Input();

            Console.WriteLine("Ladataan osaketietoja...");
            Console.WriteLine();

            //string json = File.ReadAllText("..\\..\\json\\Stocks.json");

            ////JObject o = JObject.Parse(json);

            ////JArray a = (JArray)o["d"];
            //JArray a = JArray.Parse(json);
            ////List<Stock> sto = a.ToObject<List<Stock>>();
            //List<Stock> sto = new List<Stock>();

            //foreach (var s in a) {
            //    Stock stock = new Stock();
            //    stock.ExchangeID = s["ExchangeID"].ToString();
            //    stock.BuyPrice = s.Value<double>("BuyPrice");
            //    stock.BuyDate = s["BuyDate"].ToString();
            //    sto.Add(stock);
            //}

            //foreach (var stock in sto) {
            //    stocks.Add(stock);
            //}

            return new Load(u);
        }

        internal static void OptionMenu(List<Stock> stocks, User user) {
            Console.WriteLine("Kirjauduttu käyttäjänä " + user.Username);

            while (true) {
                Console.Write("Valitse toiminto: (osta osake[b], poistu[EXIT]) ");
                string command = Console.ReadLine();

                if (command == "b" || command == "B") {
                    BuyStock(stocks);
                } else if (command == "exit" || command == "") {
                    break;
                } else {
                    Console.WriteLine("Hei, et syöttänyt kelpaavaa komentoa!");
                }
            }
        }

        private static void BuyStock(List<Stock> stocks) {

            Console.Write("Anna pörssitunnus: ");
            string exID = Console.ReadLine();

            Console.Write("Anna nimi: ");
            string name = Console.ReadLine();

            Console.Write("Anna ostovaluutta: ");
            string curr = Console.ReadLine();

            Console.Write("Anna ostohetken valuuttakurssi: ");
            double exRa = Convert.ToDouble(Console.ReadLine());

            Console.Write("Anna ostokurssi: ");
            double bPr = Convert.ToDouble(Console.ReadLine());

            stocks.Add(new Stock(exID, name, curr, exRa, bPr, "01.01.1700"));

            Console.WriteLine();
        }
    }
}
