using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps {
    internal class Stocklist {
        public List<Stock> Stocks { get; set; }

        public Stocklist() {
            this.Stocks = new List<Stock>();
        }

        public void Add(Stock stock) {
            this.Stocks.Add(stock);
        }
    }
}
