using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using atmauto.Entity;
using System.Net.Http.Headers;

namespace atmauto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.LoginForm());

             //RunAsync().GetAwaiter().GetResult();
        }

        //static HttpClient client = new HttpClient();

        /*static void ShowCabang(Cabang cabang)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine($"Cabang: {cabang.Id_Cabang}\tNama_Cabang: "
                + $"{cabang.Nama_Cabang}\tAlamat: {cabang.Alamat_Cabang}\tTelepon: {cabang.Telepon_Cabang}");
        }

        static async Task<Uri> CreateBranchAsync(Cabang cabang)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/cabangs/store", cabang);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task<Cabang> GetBranchAsync(string path)
        {
            HttpClient client = new HttpClient();
            Cabang cabang = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                cabang = await response.Content.ReadAsAsync<Cabang>();
            }
            return cabang;

        }

        static async Task<Cabang> UpdateBranchAsync(Cabang cabang)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/cabangs/update/{cabang.Id_Cabang}", cabang);
            response.EnsureSuccessStatusCode();

            cabang = await response.Content.ReadAsAsync<Cabang>();
            return cabang;

        }
        static async Task<HttpStatusCode> DeleteBranchAsync(string id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/cabangs/delete/{id}");
            return response.StatusCode;
        }

        static async Task RunAsync()
        {
            HttpClient client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://10.53.4.140:8000");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //Create a new product
                Cabang cabang = new Cabang
                (
                    "1",
                    "LOLY",
                    "TEST BISA",
                    "091291029"
                );

                var url = await CreateBranchAsync(cabang);
                Console.WriteLine($"Created at {url}");

                // Get the product
                cabang = await GetBranchAsync(url.PathAndQuery);
                ShowCabang(cabang);

                // Update the product
                Console.WriteLine("Updating telepon...");
                cabang.Telepon_Cabang = "9000";
                await UpdateBranchAsync(cabang);

                // Get the updated product
                cabang = await GetBranchAsync(url.PathAndQuery);
                ShowCabang(cabang);

                // Delete the product
                var statusCode = await DeleteBranchAsync(cabang.Id_Cabang);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        */
    }
}
