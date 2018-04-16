using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps {
    internal partial class BuyStock : Form {
        private Stocklist Stocks { get; set; }

        public BuyStock(Stock stock, User user) {
            this.Stocks = user.Stocks;
            InitializeComponent();
            lblStockName.Text = stock.ExchangeID;
            lblStockPrice.Text = stock.SharePrice.ToString();
            txtBuyPrice.Text = stock.SharePrice.ToString();

            txtQuantity.KeyDown += KeyDown;

        }

        public void KeyDown(object sender, KeyEventArgs o) {
            if (o.KeyData == Keys.Enter) {
                lblPrice.Text = (Convert.ToInt16(txtQuantity.Text) * Convert.ToDouble(txtBuyPrice.Text)).ToString();
            }
        }

        private void btnBuy_Click(object sender, EventArgs e) {
            this.Stocks.Add(new Stock(lblStockName.Text, Convert.ToDouble(txtBuyPrice.Text), long.Parse(txtQuantity.Text)));
            // sulje ikkuna
            this.Close();
        }
    }
}
