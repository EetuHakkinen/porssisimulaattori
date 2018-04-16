namespace ps {
    internal class User {
        public string Username { get; }
        public string Password { get; }
        public Stocklist Stocks { get; set; }

        public User(string username, string password, Stocklist stocks) {
            this.Username = username;
            this.Password = password;
            this.Stocks = stocks;
        }

        public User(string username, string password) {
            this.Username = username;
            this.Password = password;
            this.Stocks = new Stocklist();
        }

        public override string ToString() {
            return this.Username;
        }

        public bool CheckPassword(string pw) {
            if (this.Password == pw) {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj) {
            if (this != obj) {
                return false;
            }

            if (this.GetType() != obj.GetType()) {
                return false;
            }

            User user = (User)obj;

            if (this.Username != user.Username) {
                return false;
            }

            return true;
        }

        public override int GetHashCode() {
            return this.Username.GetHashCode();
        }
    }
}