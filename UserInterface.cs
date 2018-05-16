using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps {
    class UserInterface {
        public static void Start() {

            Load l = Features.Loadings(new Stocklist());
            Console.WriteLine();
            Console.WriteLine("**********************");
            Console.WriteLine("**PÖRSSISIMULAATTORI**");
            Console.WriteLine("**********************");
            Console.WriteLine();

            Loginpage.Login(l.Stocks, l.Userlist);
        }
    }
}
