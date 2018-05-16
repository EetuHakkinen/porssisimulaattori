namespace ps {
    public class Stock {
        private string exchangeID { get; set; }
        private string name { get; set; }
        private string currency { get; set; }
        private double exchangeRate { get; set; }
        private double buyPrice { get; set; }
        private string buyDate { get; set; }

        public Stock(string exchangeID, string name, string currency, double exchangeRate, double buyPrice, string buyDate) {
            this.exchangeID = exchangeID;
            this.name = name;
            this.currency = currency;
            this.exchangeRate = exchangeRate;
            this.buyPrice = buyPrice;
            this.buyDate = buyDate;
        }

        public override string ToString() {
            return "Tunnus: " + this.exchangeID + ", Nimi: " + this.name + ", Valuutta: " + this.currency + ", Vaihtokurssi: " + this.exchangeRate + ", Ostohinta: " + this.buyPrice + ", Ostopäivä: " + this.buyDate;
        }
    }
}