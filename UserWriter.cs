using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps {
    internal class UserWriter {
        public static void Write(Userlist users) {
            List<User> userlist = users.Users;
            JArray d = new JArray();
            foreach (var usr in userlist) {
                d.Add(JToken.FromObject(usr));
            }

            string json = d.ToString();

            using (StreamWriter writer = new StreamWriter("..\\..\\json\\Userdata.json", false)) {
                writer.Write(json);
            }

            Console.WriteLine("Käyttäjätiedot kirjattu");
        }
    }
}
