using atmauto.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Laporan
{
    public partial class FormLaporanPendapatanBulanan : Form
    {
        public LaporanPendapatanBulanan pb = new LaporanPendapatanBulanan();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanPendapatanBulanan(string year)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/pendapatan_bulanan_desktop/" +year));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());

            DataColumn col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = "year";
            dt2.Columns.Add(col);

            DataRow row;
            row = dt2.NewRow();
            row["year"] = year;
            dt2.Rows.Add(row);
            
            pb.Database.Tables["pendapatan_bulanan"].SetDataSource(dt1);
            pb.Database.Tables["year"].SetDataSource(dt2);
            crystalReportViewer1.ReportSource = pb;
        }
        
    }
}
