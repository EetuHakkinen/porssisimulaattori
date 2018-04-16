using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps {
    internal class WebReader {
        public static double ReadPage(string page) {

            Console.WriteLine("Aloitetaan");
            string json = "";
            using (WebClient wc = new WebClient()) {
                try {
                    json = wc.DownloadString(page);
                } catch (Exception e) {
                    MessageBox.Show(
                        "Sivua ei pystytä juuri nyt lataamaan!\nYritä myöhemmin uudestaan\n" + e.Message,
                        "Virhe luettaessa tietoja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                }
                
            }
            Console.WriteLine("Tiedosto luettu");

            JObject o = JObject.Parse(json);
            var a = (JObject)o["Time Series (60min)"];
            if (a == null) {
                Console.WriteLine("Virhe luettaessa sivua: " + o["Error Message"]);
                return 0;
            }
            Console.WriteLine("Parsinta 1");
            List<JObject> objs = new List<JObject>();
            List<string> keys = new List<string>();
            foreach (var i in a) {
                objs.Add((JObject)i.Value);
                keys.Add(i.Key);
            }
            Console.WriteLine("Parsinta 2");

            double number = Calculate(objs, keys);

            Console.WriteLine("Sivu " + page + " luettu");

            if (o.ContainsKey("Error")) {
                return 0;
            }
            return number;
        }

        public static double Calculate(List<JObject> objs, List<string> keys) {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            string month = yesterday.Month.ToString();
            if (yesterday.Month < 10) {
                month = "0" + yesterday.Month.ToString();
            }
            string yesterdayString = yesterday.Year.ToString() + "-" + month + "-" + yesterday.Day.ToString();
            int i = 0;
            int q = 0;

            foreach (string v in keys) {
                if (v.Contains(yesterdayString)) {
                    i = q;
                }
                q++;
            }

            double yesterdayValue = Convert.ToDouble(objs[i]["4. close"]);

            double todayValue = Convert.ToDouble(objs[0]["1. open"]);

            Console.WriteLine(todayValue);
            Console.WriteLine(yesterdayValue);

            double p = todayValue / yesterdayValue;

            double s = p - 1;

            return Math.Round(s * 100, 2);
        }
        public static double ReadCurrencyPage(string page) {

            Console.WriteLine("Aloitetaan");
            string json;
            using (WebClient wc = new WebClient()) {
                json = wc.DownloadString(page);
            }
            Console.WriteLine("Tiedosto luettu");

            JObject o = JObject.Parse(json);
            var a = (JObject)o["Realtime Currency Exchange Rate"];
            if (a == null) {
                Console.WriteLine("Virhe luettaessa sivua: " + o["Error Message"]);
                return 0;
            }
            
            Console.WriteLine("Parsinta 1");
            List<string> objs = new List<string>();
            foreach (var i in a) {
                objs.Add((string)i.Value);
            }
            Console.WriteLine(objs[4]);
            if (o.ContainsKey("Error")) {
                return 0;
            }
            return Math.Round(double.Parse(objs[4], CultureInfo.InvariantCulture), 4);
        }

        public static double ReadCryptoCurrencyPage(string page) {

            Console.WriteLine("Aloitetaan");
            string json;
            using (WebClient wc = new WebClient()) {
                json = wc.DownloadString(page);
            }
            Console.WriteLine("Tiedosto luettu");

            JObject o = JObject.Parse(json);
            var a = (JObject)o["Time Series (Digital Currency Daily)"];
            if (a == null) {
                Console.WriteLine("Virhe luettaessa sivua: " + o["Error Message"]);
                return 0;
            }
            Console.WriteLine("Parsinta 1");
            List<JObject> objs = new List<JObject>();
            List<string> keys = new List<string>();
            foreach (var i in a) {
                objs.Add((JObject)i.Value);
                keys.Add(i.Key);
            }
            Console.WriteLine("Parsinta 2");

            double number = CalculateCC(objs, keys);

            Console.WriteLine("Sivu " + page + " luettu");

            if (o.ContainsKey("Error")) {
                return 0;
            }
            return number;
        }

        public static double CalculateCC(List<JObject> objs, List<string> keys) {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            string month = yesterday.Month.ToString();
            if (yesterday.Month < 10) {
                month = "0" + yesterday.Month.ToString();
            }
            string yesterdayString = yesterday.Year.ToString() + "-" + month + "-" + yesterday.Day.ToString();
            int i = 0;
            int q = 0;

            foreach (string v in keys) {
                if (v.Contains(yesterdayString)) {
                    i = q;
                }
                q++;
            }

            double yesterdayValue = Convert.ToDouble(objs[i]["4a. close (EUR)"]);

            double todayValue = Convert.ToDouble(objs[0]["1a. open (EUR)"]);

            Console.WriteLine(todayValue);
            Console.WriteLine(yesterdayValue);

            double p = todayValue / yesterdayValue;

            double s = p - 1;

            return Math.Round(s * 100, 2);
        }

        public static Stock FindStock(string stockID) {
            string page = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + stockID + "&interval=60min&apikey=8YFH2JQFGQIM9T4L";
            string currencyPage = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=EUR&to_currency=" + stockID + "&apikey=8YFH2JQFGQIM9T4L";

            if (!page.Contains("Error")) {
                string json;
                using (WebClient wc = new WebClient()) {
                    json = wc.DownloadString(page);
                }
                Console.WriteLine("Tiedosto " + page + " luettu");

                if (json.Contains("Error")) {
                    MessageBox.Show(
                        "Osakkeen tietoja ei löydy juuri nyt.\nYritä myöhemmin uudestaan!",
                        "Virhe luettaessa osakkeen tietoja!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                }

                JObject o = JObject.Parse(json);

                var a = (JObject)o["Time Series (60min)"];
                if (a == null) {
                    Console.WriteLine("Virhe luettaessa sivua: " + o["Error Message"]);
                    return null;
                }

                List<JObject> objs = new List<JObject>();
                List<string> keys = new List<string>();
                foreach (var i in a) {
                    objs.Add((JObject)i.Value);
                    keys.Add(i.Key);
                }

                DateTime yesterday = DateTime.Today.AddDays(-1);
                string month = yesterday.Month.ToString();
                if (yesterday.Month < 10) {
                    month = "0" + yesterday.Month.ToString();
                }
                string yesterdayString = yesterday.Year.ToString() + "-" + month + "-" + yesterday.Day.ToString();

                int index = 0;
                int q = 0;

                foreach (string v in keys) {
                    if (v.Contains(yesterdayString)) {
                        index = q;
                    }
                    q++;
                }

                double yesterdayValue = Convert.ToDouble(objs[index]["4. close"]);

                double todayValue = Convert.ToDouble(objs[0]["1. open"]);

                double change = (todayValue / yesterdayValue - 1) * 100;

                List<double> doubles = new List<double>();
                for (int x = 0; x < objs.Count; x++) {
                    doubles.Add(Convert.ToDouble(objs[x]["4. close"]));
                }

                List<long> volumes = new List<long>();
                for (int i = 0; i < objs.Count; i++) {
                    volumes.Add(Convert.ToInt64(objs[i]["5. volume"]));
                }

                return new Stock(stockID, Convert.ToDouble(objs[0]["4. close"]), change, doubles, keys, volumes);
            } else if (!currencyPage.Contains("Error")) {
                string json;
                using (WebClient wc = new WebClient()) {
                    json = wc.DownloadString(currencyPage);
                }
                Console.WriteLine("Tiedosto " + currencyPage + " luettu");

                if (json.Contains("Error")) {
                    MessageBox.Show(
                        "Osakkeen tietoja ei löydy juuri nyt.\nYritä myöhemmin uudestaan!",
                        "Virhe luettaessa osakkeen tietoja!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                }

                JObject o = JObject.Parse(json);

                var a = (JObject)o["Realtime Currency Exchange Rate"];
                if (a == null) {
                    Console.WriteLine("Virhe luettaessa sivua: " + o["Error Message"]);
                    return null;
                }

                List<JObject> objs = new List<JObject>();
                List<string> keys = new List<string>();
                foreach (var i in a) {
                    objs.Add((JObject)i.Value);
                    keys.Add(i.Key);
                }



                //double todayValue = Convert.ToDouble(objs[0]["1. open"]);

                return new Stock(stockID, Convert.ToDouble(objs[5]), 0, new List<double>(), new List<string>(), new List<long>());
            }

            return new Stock();
            
        }

    }

}