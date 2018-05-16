using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ps {
    internal partial class Porssisimulaattori : Form {
        Load l;
        private object reloadLock = new object();
        string username;
        User user;
        Stock stock;
        Stocklist stocklist;
        Userlist users;
        List<Stock> loadedStocks;

        public Porssisimulaattori() {
            this.stocklist = new Stocklist();
            this.l = Features.Loadings(this.stocklist);
            this.users = l.Userlist;
            InitializeComponent();
            lblLogIn.Text = "Kirjaudu sisään";

            txtPassword.KeyDown += KeyDown;
            txtUsername.KeyDown += KeyDown;

            Update();

            this.loadedStocks = new List<Stock>();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }

        public bool LoginGUI(string username, string password, List<Stock> stocks) {
            if (this.users.Search(username)) {
                if (this.users.SearchUser(username) == null) {
                    Console.WriteLine("Käyttäjää ei löytynyt!");
                    return false;
                }
                if (Password.CheckPassword(password, username, users)) {
                    this.user = this.users.SearchUser(username);
                    return true;
                }

            }
            return false;
        }

        private void lblLoginFail_Click(object sender, EventArgs e) {
            
        }
        new private void KeyDown(object sender, KeyEventArgs o) {
            if (o.KeyData == Keys.Enter) {
                
                string pw = txtPassword.Text;
                string usnm = txtUsername.Text;

                bool on = this.LoginGUI(usnm, pw, l.Stocks);

                if (!on) {
                    System.Threading.Thread.Sleep(5000);
                    MessageBox.Show(
                        "Kirjautuminen epäonnistui!",
                        "Virhe!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                } else {
                    pnlLogin.Hide();
                    lblLoggedIn.Show();
                    btnLogOut.Show();
                    lblLoggedIn.Location = new Point(20, 20);
                    lblLoggedIn.Text = "Kirjauduttu käyttäjänä " + txtUsername.Text;
                    username = usnm;
                    if (pnlFindStock.Visible) {
                        btnBuyStock.Show();
                    }
                    btnShowPortfolio.Show();
                }
            }
        }

        private void btnAdduser_Click(object sender, EventArgs e) {
            this.Hide("AddUser");
            // käyttäjän lisääminen
            pnlAddUser.Show();
            txtAddUserPassword.KeyDown += KeyDown2;
            txtAddUserUsername.KeyDown += KeyDown2;
            
            
        }

        private void btnLogOut_Click(object sender, EventArgs e) {
            btnLogOut.Hide();
            lblLoggedIn.Hide();
            pnlLogin.Show();
            txtPassword.Text = "";
            txtUsername.Text = "";
            this.user = null;
            btnBuyStock.Hide();
            btnShowPortfolio.Hide();
            pnlPortfolio.Hide();
        }

        private void KeyDown2(object sender, KeyEventArgs o) {
            this.Hide("AddUser");
            if (o.KeyData == Keys.Enter) {
                string username = txtAddUserUsername.Text;
                string password = txtAddUserPassword.Text;

                string savedPasswordHash = Password.Hash(password);

                if (!l.Userlist.Search(username)) {
                    l.Userlist.Users.Add(new User(username, savedPasswordHash));
                    MessageBox.Show(
                        "Käyttäjä " + username + " luotu",
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                    );
                    pnlAddUser.Hide();
                } else {
                    MessageBox.Show(
                        "Käyttäjän luominen epäonnistui! \nKäyttäjänimi on varattu",
                        "Virhe!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                }
            }
        }

        private void lnklblCloseCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            pnlAddUser.Hide();
        }

        private void lnklblUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Update();
        }

        public void Update() {

            lnklblUpdate.Text = "Ladataan...";
            Task.Run(() => {
                lock (reloadLock) {
                    double oMXH25 = WebReader.ReadPage("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=OMXH25&interval=60min&outputsize=compact&apikey=8YFH2JQFGQIM9T4L");
                    double sP500 = WebReader.ReadPage("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=SPX&interval=60min&outputsize=compact&apikey=8YFH2JQFGQIM9T4L");
                    double eURUSD = WebReader.ReadCurrencyPage("https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=EUR&to_currency=USD&apikey=8YFH2JQFGQIM9T4L");
                    double bitcoin = WebReader.ReadCryptoCurrencyPage("https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol=BTC&market=EUR&apikey=8YFH2JQFGQIM9T4L");
                    Invoke(new Action(() => {
                        if (oMXH25 != 0) {
                            lblOMXH25Value.Text = oMXH25.ToString() + " %";
                            lblOMXH25Value.Color();
                        }
                        if (sP500 != 0) {
                            lblSP500Value.Text = sP500.ToString() + " %";
                            lblSP500Value.Color();
                        }
                        if (eURUSD != 0) {
                            lblEURUSD.Text = eURUSD.ToString();
                        }
                        if (bitcoin != 0) {
                            lblBitcoinCurrency.Text = bitcoin.ToString() + " %";
                            lblBitcoinCurrency.Color();
                        }
                        lnklblUpdate.Text = "Päivitä";
                    }));
            }
            });
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.Hide("FindStock");
                string item = txtFind.Text;

                bool containslist = false;
                Stock stock1 = null;
                foreach (var stock in this.loadedStocks) {
                    if (stock.ExchangeID == item) {
                        containslist = true;
                        stock1 = stock;
                    }
                }

                Stock s;

                if (containslist) {
                    s = stock1;
                } else {
                    s = WebReader.FindStock(item);
                }

                if (s != null) {
                    pnlFindStock.Show();
                    if (this.user != null) {
                        btnBuyStock.Show();
                    }
                    this.stock = s;
                    lblItem.Text = s.ExchangeID;
                    lblStockPrice.Text = Math.Round(s.SharePrice, 2).ToString();
                    lblStockChange.Text = Math.Round(s.Change, 2).ToString() + " %";
                    lblStockChange.Color();
                    lblStockPrice.Color();

                    lblCorporationName.Text = s.Name;
                    lblCurrency.Text = "Valuutta: " + s.Currency;

                    graph.Series.Clear();
                    crtVolumes.Series.Clear();
                    Series Minimum = graph.Series.Add("Graph");
                    Series Volumes = crtVolumes.Series.Add("Volume");

                    List<string> keys = new List<string>();
                    List<double> chart = new List<double>();
                    List<long> volume = new List<long>();

                    for (int y = s.Keys.Count - 1; y > 0; y--) {
                        keys.Add(s.Keys[y]);
                        chart.Add(s.Chart[y]);
                        volume.Add(s.Volumes[y]);
                    }

                    Minimum.Points.DataBindXY(keys, chart);
                    Minimum.ChartType = SeriesChartType.Line;
                    //graph.Scale(new SizeF(500f, 500f));
                    graph.Legends.Clear();
                    // graph.ChartAreas[0].AlignmentOrientation = AreaAlignmentOrientations.Vertical;

                    double yMax = s.Chart.Max() + 1;
                    double yMin = s.Chart.Min() - 1;

                    graph.ChartAreas[0].AxisY.Maximum = yMax;
                    graph.ChartAreas[0].AxisY.Minimum = yMin;

                    Volumes.Points.DataBindXY(keys, volume);
                    Volumes.ChartType = SeriesChartType.Column;

                    crtVolumes.Legends.Clear();

                    double vyMax = s.Volumes.Max() + 1;
                    double vyMin = 0;

                    crtVolumes.ChartAreas[0].AxisY.Maximum = vyMax;
                    crtVolumes.ChartAreas[0].AxisY.Minimum = vyMin;
                    this.loadedStocks.Add(s);
                }
            }
        }

        public void Hide(string what) {
            if (what != "AddUser") {
                pnlAddUser.Hide();
            }
            if (what != "FindStock") {
                pnlFindStock.Hide();
            }
            if (what != "Portfolio") {
                pnlPortfolio.Hide();
            }
            
        }

        private void btnBuyStock_Click(object sender, EventArgs e) {
            new BuyStock(this.stock, this.user).ShowDialog();
        }

        private void Closing(object sender, FormClosingEventArgs e) {
            UserWriter.Write(this.users);
        }

        private void btnShowPortfolio_Click(object sender, EventArgs e) {
            // Tee oman salkun näkymä
            this.Hide("Portfolio");
            pnlPortfolio.Show();
            lblPortfolioStocks.Text = string.Join("\n", from s in this.user.Stocks.Stocks select s.ExchangeID);
        }
    }
}