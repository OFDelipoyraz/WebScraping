using Microsoft.AspNetCore.Mvc;
using Scraping.Scrapingg;
using System.Data.SqlClient;

namespace Scraping.Controllers
{
    public class ListController : Controller
    {
        private SqlConnection connection = new SqlConnection("Data Source = DESKTOP - 98HOOEP; Initial Catalog = WebScrapingProject; Integrated Security = True");
        public IActionResult Index()
        {
            WebScraping Scraping = new WebScraping();
            List<String> Liste =Scraping.Trendyol();
            ViewData["Trendyol"] =Liste;
            

            return View();
        }
        private void Add_Data(string Marka, string Fiyat, string Link)
        {
            connection.Open();
            string sql = "INSERT INTO Deneme1 (Marka,Fiyat,Link) values (@marka,@fiyat,@link),";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("Marka", Marka);
            cmd.Parameters.AddWithValue("Fiyat", Fiyat);
            cmd.Parameters.AddWithValue("Link",Link);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
