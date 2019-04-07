using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atmauto.Entity
{
    class Login
    {
        public int Id_Pegawai;
        public string Username;
        public string Password;
        

        public class Data
        {
            public Data(string json)
            {
                JObject jObject = JObject.Parse(json);
                JToken jUser = jObject["data"];
                name = (string)jUser["Nama_Pegawai"];
                username = (string)jUser["Username"];
                id = (int)jUser["Id_Pegawai"];
                role = (string)jUser["Nama_Role"];
            }
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string role { get; set; }
        }
    }
}
