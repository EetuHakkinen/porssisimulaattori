using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps {
    static internal class Extensions {
        public static void Color(this Label t) {
            if (Convert.ToDouble(t.Text.Split(' ')[0]) > 0) {
                t.ForeColor = System.Drawing.Color.Green;
            } else if (Convert.ToDouble(t.Text.Split(' ')[0]) < 0) {
                t.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
