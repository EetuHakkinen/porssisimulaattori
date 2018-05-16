using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porssisimulaattori {
    internal class Login {
        public static bool ChechPassword(string username, string password) {

            

            return false;
        }

        public static Load Load() {
            Userlist u = new Userlist();
            u.Input();

            Console.WriteLine("Ladataan osaketietoja...");
            Console.WriteLine();

            string json = File.ReadAllText("..\\..\\json\\Stocks.json");

            JObject o = JObject.Parse(json);

            JArray a = (JArray)o["d"];

            List<Stock> sto = a.ToObject<List<Stock>>();

            return new Load(sto, u);
        }
    }
}
