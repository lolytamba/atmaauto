using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atmauto.Entity
{
    class Sparepart
    {

        public string Kode_Sparepart;
        public string Tipe_Barang;
        public string Nama_Sparepart;
        public string Merk_Sparepart;
        public string Rak_Sparepart;
        public string Jumlah_Sparepart;
        public int Stok_Minimum_Sparepart;
        public int Harga_Beli;
        public int Harga_Jual;
        public string Gambar;

        public string kode_sparepart { get; set; }
        public string tipe_barang { get; set; }
        public string nama_sparepart { get; set; }
        public string merk_sparepart { get; set; }
        public string rak_sparepart { get; set; }
        public int jumlah_sparepart { get; set; }
        public string stok_minimum_sparepart { get; set; }
        public float harga_beli { get; set; }
        public float harga_jual { get; set; }
        public string gambar { get; set; }
    }
}
