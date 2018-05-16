using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps{
    class Porssi{

        static void Main(string[] args){

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            //UserInterface.Start();

            //Console.WriteLine("Syötä salasana: ");
            //string pw = Console.ReadLine();

            //Console.WriteLine(Password.Hash(pw));
            //Console.WriteLine(SecurePasswordHasher.Verify(pw, Password.Hash(pw)));
            //Console.Read();

            Application.Run(new Porssisimulaattori());

            //Stocklist s = new Stocklist();

            //s.Add(new Stock("FORTUM", 16.52, 5));
            //s.Add(new Stock("NDA1V", 10.50, 10));

            //StockWriter.Write(s);

            //Console.ReadLine();

            //WebReader.FindStock("MSFT");

            //Console.ReadLine();
        }
    }
}
