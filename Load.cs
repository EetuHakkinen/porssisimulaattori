using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps {
    internal class Load {
        public List<Stock> Stocks { get; set; }
        public Userlist Userlist { get; set; }

        public Load(Userlist u) {
            this.Userlist = u;
        }
    }
}
