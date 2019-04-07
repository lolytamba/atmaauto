using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atmauto.Entity
{
    class Pegawai
    {
        public string Id_Pegawai;
        public string Id_Role;
        public string Id_Cabang;
        public string Nama_Pegawai;
        public string Alamat_Pegawai;
        public string Telepon_Pegawai;
        public double Gaji_Pegawai;
        public string Username;
        public string Password;

        public string id_pegawai { get; set; }
        public string id_role { get; set; }
        public string id_cabang { get; set; }
        public string nama_pegawai { get; set; }
        public string alamat_pegawai { get; set; }
        public string telepon_pegawai { get; set; }
        public double gaji_pegawai { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
