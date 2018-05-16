using System;
using System.Collections.Generic;

namespace ps {
    internal class Stock {
        public string ExchangeID { get; internal set; }
        public string Name { get; internal set; }
        public string Currency { get; internal set; }
        public double ExchangeRate { get; internal set; }
        public double BuyPrice { get; internal set; }
        public string BuyDate { get; internal set; }
        public long Quantity { get; set; }
        public double SharePrice { get; internal set; }
        public double Change { get; internal set; }
        public List<double> Chart { get; internal set; }
        public List<string> Keys { get; internal set; }
        public List<long> Volumes { get; internal set; }

        public Stock(string exchangeID, string name, string currency, double exchangeRate, double buyPrice, string buyDate) {
            this.ExchangeID = exchangeID;
            this.Name = name;
            this.Currency = currency;
            this.ExchangeRate = exchangeRate;
            this.BuyPrice = buyPrice;
            this.BuyDate = buyDate;
            this.Chart = new List<double>();
            this.Keys = new List<string>();
        }

        public Stock(string exchangeID, double buyPrice, long quantity) {
            this.ExchangeID = exchangeID;
            this.BuyPrice = buyPrice;
            this.BuyDate = DateTime.Now.ToString();
            this.Quantity = quantity;
        }


        public Stock() {
            this.ExchangeID = "";
            this.Chart = new List<double>();
            this.Keys = new List<string>();
        }

        public Stock(string exchangeID, double sharePrice, double change, List<double> chart, List<string> keys, List<long> volumes) {
            this.ExchangeID = exchangeID;
            this.Name = "";
            this.Currency = "EUR";
            this.ExchangeRate = 1;
            this.SharePrice = sharePrice;
            this.Change = change;
            this.Chart = chart;
            this.Keys = keys;
            this.Volumes = volumes;
        }

        public override string ToString() {
            return "Tunnus: " + this.ExchangeID + ", Nimi: " + this.Name + ", Valuutta: " + this.Currency + ", Vaihtokurssi: " + this.ExchangeRate + ", Ostohinta: " + this.BuyPrice + ", Ostopäivä: " + this.BuyDate;
        }
    }
}