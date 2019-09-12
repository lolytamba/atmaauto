using atmauto.Entity;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Laporan
{
    public partial class FormCetakSuratPerintahKerja : Form
    {
        public SuratPerintahKerja SPK = new SuratPerintahKerja();
        static WebHelper webHelper = new WebHelper();

        public FormCetakSuratPerintahKerja(string id)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/cetakSuratPerintahKerjaDesktop/" +id));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();


            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data1").ToString());
            if (dt1.Rows.Count == 0)
            {
                string dSparepart = "[{Id_Transaksi: '-',Kode: '-',Nama: '-',Merk: '-',Rak: '-', Jumlah: 0}]";
            }
            dt2 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data2").ToString());
            if (dt2.Rows.Count == 0)
            {
                string dJasa = "[{Id_Transaksi: '-',KodeJasa: '-',NamaJasa: '-'}]";
            }
            dt3 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data3").ToString());
            dt4 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data4").ToString());
            
            dt5 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data5").ToString());
            if (dt5.Rows.Count == 0)
            {
                string dMontirSparepart = "[{Id_Transaksi: '-',Montir: '-'}]";
            }
            dt6 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data6").ToString());
            if (dt6.Rows.Count == 0)
            {
                string dMontirJasa = "[{Id_Transaksi: '-',Montir: '-'}]";
            }
            dt7 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data7").ToString());
            dt8 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data8").ToString());
            if (dt8.Rows.Count == 0)
            {
                string dMotorSP = "[{Id_Transaksi: '-',Merk: '-',Tipe: '-',Plat: '-'}]";
            }
            dt9 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data9").ToString());
            if (dt9.Rows.Count == 0)
            {
                string dMotorSV = "[{Id_Transaksi: '-',Merk: '-',Tipe: '-',Plat: '-'}]";
            }
            SPK.Database.Tables["detail_spareparts"].SetDataSource(dt1);
            SPK.Database.Tables["detail_jasas"].SetDataSource(dt2);
            SPK.Database.Tables["konsumens"].SetDataSource(dt3);
            SPK.Database.Tables["pegawai_on_duties_"].SetDataSource(dt4);
            SPK.Database.Tables["pegawai_on_duties_2"].SetDataSource(dt5);
            SPK.Database.Tables["montirs"].SetDataSource(dt6);
            SPK.Database.Tables["transaksi_penjualans"].SetDataSource(dt7);
            SPK.Database.Tables["motor_konsumens"].SetDataSource(dt8);
            SPK.Database.Tables["motor_konsumens"].SetDataSource(dt9);

            crystalReportViewer1.ReportSource = SPK;

        }
    }
}
