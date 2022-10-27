using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Scraping.Controllers
{
    public class AdminController : Controller
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-98HOOEP;Initial Catalog=WebScrapingProject;Integrated Security=True");
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Update(int id, string marka, string model, string fiyat, string isletim_Sistemi, string islemci, string islemci_Modeli, string islemci_Hizi, string rAM, string rAM_Turu, string disk_Kapasitesi, string ekran_Boyutu, string cozunurluk, string agirlik, int site_Id, string link, string genel_Isim, string puan)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Teknopazar SET Marka = @marka, Model=@model,Fiyat=@fiyat ,Isletim_Sistemi= @isletim_Sistemi,Islemci=@islemci,Islemci_Modeli=@islemci_Modeli ,Islemci_Hizi=@islemci_Hizi ,RAM_Turu=@rAM_Turu, Disk_Kapasitesi=@disk_Kapasitesi,Ekran_Boyutu=@ekran_Boyutu,Cozunurluk=@cozunurluk,Agirlik=@agirlik,Link=@link WHERE Id=@id";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@id", id );
            cmd.Parameters.AddWithValue("@marka", marka ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@model", model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fiyat", fiyat ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isletim_sistemi", isletim_Sistemi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci", islemci ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_modeli", islemci_Modeli ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_hizi", islemci_Hizi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram", rAM ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram_turu", rAM_Turu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@disk_kapasitesi", disk_Kapasitesi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ekran_boyutu", ekran_Boyutu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cozunurluk", cozunurluk ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@agirlik", agirlik ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@link", link ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE  FROM Teknopazar WHERE Id = @id ";
            cmd.Parameters.AddWithValue("@id", id );
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public IActionResult Add(string marka, string model, string fiyat, string isletim_Sistemi, string islemci, string islemci_Modeli, string islemci_Hizi, string rAM, string rAM_Turu, string disk_Kapasitesi, string ekran_Boyutu, string cozunurluk, string agirlik, int site_Id, string link, string genel_Isim, string puan)
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
            cmd.Parameters.AddWithValue("@marka", marka ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@model", model ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@fiyat", fiyat ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isletim_sistemi", isletim_Sistemi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci", islemci ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_modeli", islemci_Modeli ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@islemci_hizi", islemci_Hizi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram", rAM ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ram_turu", rAM_Turu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@disk_kapasitesi", disk_Kapasitesi ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ekran_boyutu", ekran_Boyutu ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@cozunurluk", cozunurluk ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@agirlik", agirlik ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", site_Id);
            cmd.Parameters.AddWithValue("@link", link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@genel_isim", genel_Isim ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@puan", puan ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult PuanUpdate(int id, string puan)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Teknopazar SET Puan=@puan WHERE Id = @id ";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@puan",puan  ?? (object)DBNull.Value);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult FiyatUpdate(int id, string fiyat)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "UPDATE Teknopazar SET Fiyat= @fiyat WHERE Id = @id ";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@fiyat", fiyat ?? (object)DBNull.Value);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Admin");
        }
    }
}
