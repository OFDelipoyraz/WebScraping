using Microsoft.AspNetCore.Mvc;
using Scraping.Models;
using System.Data.SqlClient;

namespace Scraping.Controllers
{
    public class CompareController : Controller
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-98HOOEP;Initial Catalog=WebScrapingProject;Integrated Security=True");
        public IActionResult Index()
        {
            List<Computers> Liste = new List<Computers>();
            ViewData["pc"] = PCFetch_Data(Liste);
            
            return View();
        }
        private List<Computers> PCFetch_Data(List<Computers> Liste)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM PC";
            cmd.Connection = connection;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Liste.Add(new Computers(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Marka"]), Convert.ToString(reader["Model"]), Convert.ToString(reader["Fiyat"]), Convert.ToString(reader["Isletim_Sistemi"]), Convert.ToString(reader["Islemci"]), Convert.ToString(reader["Islemci_Modeli"]), Convert.ToString(reader["Islemci_Hizi"]), Convert.ToString(reader["RAM"]), Convert.ToString(reader["RAM_Turu"]), Convert.ToString(reader["Disk_Kapasitesi"]), Convert.ToString(reader["Ekran_Boyutu"]), Convert.ToString(reader["Cozunurluk"]), Convert.ToString(reader["Agirlik"]), Convert.ToInt32(reader["Site_Id"]), Convert.ToString(reader["Link"]), Convert.ToString(reader["Genel_Isim"]), Convert.ToString(reader["Puan"])));

            }
            connection.Close();
            return Liste;
        }
        
        [HttpPost]
        public ActionResult Search(string model)
        {
            List<Computers> Liste2 = new List<Computers>();
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT *  FROM PC WHERE Model = @model ";
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Connection = connection;
             SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Liste2.Add(new Computers(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Marka"]), Convert.ToString(reader["Model"]), Convert.ToString(reader["Fiyat"]), Convert.ToString(reader["Isletim_Sistemi"]), Convert.ToString(reader["Islemci"]), Convert.ToString(reader["Islemci_Modeli"]), Convert.ToString(reader["Islemci_Hizi"]), Convert.ToString(reader["RAM"]), Convert.ToString(reader["RAM_Turu"]), Convert.ToString(reader["Disk_Kapasitesi"]), Convert.ToString(reader["Ekran_Boyutu"]), Convert.ToString(reader["Cozunurluk"]), Convert.ToString(reader["Agirlik"]), Convert.ToInt32(reader["Site_Id"]), Convert.ToString(reader["Link"]), Convert.ToString(reader["Genel_Isim"]), Convert.ToString(reader["Puan"])));
                    
            }
            connection.Close();
            ViewData["pc"] = Liste2;
            return View();
        }
    }
}
