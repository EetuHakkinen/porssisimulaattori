using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ps {
    internal class Userlist {
        public List<User> Users { get; set; }

        public Userlist() {
            Users = new List<User>();
        }

        public void Input() {
            Console.WriteLine("Ladataan käyttäjiä...");
            Console.WriteLine();

            string json = File.ReadAllText("..\\..\\json\\Userdata.json");

            // JObject o = JObject.Parse(json);

            JArray a = JArray.Parse(json);

            foreach(var user in a) {
                string usnm = user["Username"].ToString();
                string pw = user["Password"].ToString();
                Stocklist stocklist = new Stocklist();

                JObject o = JObject.Parse(user["Stocks"].ToString());
                JArray stocks = JArray.FromObject(o["Stocks"]);
                foreach (var s in stocks) {
                    Stock newStock = new Stock();
                    newStock.ExchangeID = s["ExchangeID"].ToString();
                    newStock.BuyPrice = s.Value<double>("BuyPrice");
                    newStock.BuyDate = s["BuyDate"].ToString();
                    stocklist.Add(newStock);
                }

                this.Users.Add(new User(usnm, pw, stocklist));
            }
        }

        public bool Contains(User user) {
            if (Users.Contains(user)) {
                return true;
            }
            return false;
        }

        public bool Search(string username) {
            foreach (User user in Users) {
                if (user.Username == username) {
                    return true;
                }
            }
            return false;
        }

        public User SearchUser(string username) {
            foreach (User user in Users) {
                if (user.Username == username) {
                    return user;
                }
                
            }
            return null;
        }

        public void remove(User user) {
            Users.Remove(user);
            // POISTA KÄYTTÄJÄ JSONISTA!
        }

        public void Show() {
            foreach (User user in Users) {
                Console.WriteLine(user);
            }
        }

        public bool Empty() {
            if (Users.Count() == 0) {
                return true;
            }
            return false;
        }

        public void CreateUser() {
            string usnm;
            while (true) {
                Console.WriteLine("Syötä käyttäjänimi: ");
                usnm = Console.ReadLine();
                if (usnm == "nayta") {
                    this.Show();
                    continue;
                }
                break;
            }
            Console.WriteLine("Syötä salasana: ");
            string password = Console.ReadLine();
            
            string savedPasswordHash = Password.Hash(password);

            Users.Add(new User(usnm, savedPasswordHash));
            Console.WriteLine("Uusi käyttäjä " + usnm + " luotu.");
            Console.WriteLine("HUOM! Käyttäjä on luotu vain tämän istunnon ajaksi, sillä toiminto sen tallentamiseen puuttuu!");
            Console.WriteLine("Muista kirjata käyttäjän tiedot Userdata.json-tiedostoon");
        }

        public void GCreateUser(string username, string password, Load l) {
            
        }
    }
}
