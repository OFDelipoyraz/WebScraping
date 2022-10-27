using Microsoft.AspNetCore.Mvc;
using Scraping.Models;
using Scraping.Scrapingg;
using System.Data.SqlClient;

namespace Scraping.Controllers
{
    public class ListController : Controller
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-98HOOEP;Initial Catalog=WebScrapingProject;Integrated Security=True");
        public IActionResult Index()
        {


            //Teknosa Scraping = new Teknosa();
            //List<Computers> Liste = Scraping.teknosa();
            //N11 Scraping = new N11();
            //List<Computers> Liste = Scraping.n11();
            //Amazon Scraping = new Amazon();
            //List<Computers> Liste = Scraping.amazon();
            //Vatan Scraping = new Vatan();
            //List<Computers> Liste = Scraping.vatan();

            //DeleteData();
            //PCDeleteData();
            /*
           foreach (Computers Computers in Liste)
            {
                Add_Data(Computers);
            }*/
            
            List<Computers> Liste = new List<Computers>();

           ViewData["PC"] = Fetch_Data(Liste);
            

            return View();
        }

        private List<Computers> Fetch_Data(List<Computers> Liste)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM Teknopazar";
            cmd.Connection = connection;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Liste.Add(new Computers(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Marka"]), Convert.ToString(reader["Model"]), Convert.ToString(reader["Fiyat"]), Convert.ToString(reader["Isletim_Sistemi"]), Convert.ToString(reader["Islemci"]), Convert.ToString(reader["Islemci_Modeli"]), Convert.ToString(reader["Islemci_Hizi"]), Convert.ToString(reader["RAM"]), Convert.ToString(reader["RAM_Turu"]), Convert.ToString(reader["Disk_Kapasitesi"]), Convert.ToString(reader["Ekran_Boyutu"]), Convert.ToString(reader["Cozunurluk"]), Convert.ToString(reader["Agirlik"]), Convert.ToInt32(reader["Site_Id"]), Convert.ToString(reader["Link"]), Convert.ToString(reader["Genel_Isim"]), Convert.ToString(reader["Puan"])));
                    
            }
            connection.Close();
            return Liste;
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

        private void DeleteData()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM Teknopazar";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        private void PCDeleteData()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM PC";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        private void PCAdd_Data(Computers computers)

        {
            
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT PC (Marka," +
                "Model," +
                "Fiyat," +
                "Isletim_Sistemi," +
                "Islemci," +
                "Islemci_Modeli," +
                "Islemci_Hizi," +
                "RAM," +
                "RAM_Turu," +
                "Disk_Kapasitesi," +
                "Ekran_Boyutu," +
                "Cozunurluk," +
                "Agirlik," +
                "Site_Id," +
                "Link," +
                "Genel_Isim," +
                "Puan) " +
                "VALUES " +
                "(@marka," +
                "@model,"+
                "@fiyat," +
                "@isletim_sistemi," +
                "@islemci," +
                "@islemci_modeli," +
                "@islemci_hizi," +
                "@ram," +
                "@ram_turu," +
                "@disk_kapasitesi," +
                "@ekran_boyutu," +
                "@cozunurluk," +
                "@agirlik," +
                "@site_id," +
                "@link," +
                "@genel_isim," +
                "@puan)";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@marka", computers.Marka ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@model", computers.Model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fiyat", computers.Fiyat ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isletim_sistemi", computers.Isletim_Sistemi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci", computers.Islemci ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_modeli", computers.Islemci_Modeli ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_hizi", computers.Islemci_Hizi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram", computers.RAM ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram_turu", computers.RAM_Turu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@disk_kapasitesi", computers.Disk_Kapasitesi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ekran_boyutu", computers.Ekran_Boyutu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cozunurluk", computers.Cozunurluk ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@agirlik", computers.Agirlik ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", computers.Site_Id  );
            cmd.Parameters.AddWithValue("@link", computers.Link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@genel_isim", computers.Genel_Isim ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@puan", computers.Puan ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
        private void Add_Data(Computers computers)

        {

            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Teknopazar (Marka," +
                "Model," +
                "Fiyat," +
                "Isletim_Sistemi," +
                "Islemci," +
                "Islemci_Modeli," +
                "Islemci_Hizi," +
                "RAM," +
                "RAM_Turu," +
                "Disk_Kapasitesi," +
                "Ekran_Boyutu," +
                "Cozunurluk," +
                "Agirlik," +
                "Site_Id," +
                "Link," +
                "Genel_Isim," +
                "Puan) " +
                "VALUES " +
                "(@marka," +
                "@model," +
                "@fiyat," +
                "@isletim_sistemi," +
                "@islemci," +
                "@islemci_modeli," +
                "@islemci_hizi," +
                "@ram," +
                "@ram_turu," +
                "@disk_kapasitesi," +
                "@ekran_boyutu," +
                "@cozunurluk," +
                "@agirlik," +
                "@site_id," +
                "@link," +
                "@genel_isim," +
                "@puan)";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@marka", computers.Marka ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@model", computers.Model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fiyat", computers.Fiyat ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isletim_sistemi", computers.Isletim_Sistemi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci", computers.Islemci ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_modeli", computers.Islemci_Modeli ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_hizi", computers.Islemci_Hizi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram", computers.RAM ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram_turu", computers.RAM_Turu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@disk_kapasitesi", computers.Disk_Kapasitesi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ekran_boyutu", computers.Ekran_Boyutu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cozunurluk", computers.Cozunurluk ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@agirlik", computers.Agirlik ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", computers.Site_Id);
            cmd.Parameters.AddWithValue("@link", computers.Link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@genel_isim", computers.Genel_Isim ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@puan", computers.Puan ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            connection.Close();

        }

    }
}
