using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps {
    internal class Loginpage {
        public static void Login(List<Stock> stocks, Userlist users) {
            Console.WriteLine();
            Console.WriteLine("Kirjaudu sisään ");
            Console.WriteLine();

            while (true) {
                while (true) {
                    if (!users.Empty()) {
                        break;
                    }
                    Console.WriteLine("Ei käyttäjiä.");
                    Console.Write("Haluatko luoda uuden käyttäjän? (Y/n): ");
                    string n = Console.ReadLine();
                    if (n == "y" || n == "Y" || n == "") {
                        users.CreateUser();
                    }
                }
                Console.Write("Syötä käyttäjätunnus: ");
                string username = Console.ReadLine();

                if (username == "nayta") {
                    Console.WriteLine();
                    Console.WriteLine("Valittavissa olevat käyttäjät: ");
                    users.Show();
                    Console.WriteLine();
                    continue;
                }
                
                Console.Write("Syötä salasana: ");
                string password = Console.ReadLine();
                Console.WriteLine();

                if (users.Search(username)) {
                    if (users.SearchUser(username) == null) {
                        Console.WriteLine("Käyttäjää ei löytynyt!");
                    }
                    if (Password.CheckPassword(password, username, users)) {
                        Features.OptionMenu(stocks, users.SearchUser(username));
                        break;
                    } else {
                        Console.WriteLine("Salasana väärin!");
                    }

                } else {
                    Console.WriteLine("Käyttäjää ei löytynyt!");
                }
            }
        }
    }
}
