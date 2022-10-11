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
            List<Computers> pc = new List<Computers>()
            {
            new Computers{Marka="Bill",Fiyat="1234", Link="asdff"},
            new Computers{Marka="Bille",Fiyat="1234e", Link="asdffe"},
            new Computers{Marka="Bill1",Fiyat="1234as", Link="asdffas"},
            new Computers{Marka="Billee",Fiyat="1234eass", Link="asdffase"}
            };

            WebScraping Scraping = new WebScraping();
            List<String> Liste = Scraping.Trendyol();
            ViewData["Trendyol"] = Liste;
            DeleteData();
            for (int i = 0; i < 4; i++)
            {
            Add_Data(pc[i].Marka, pc[i].Fiyat, pc[i].Link);
            }


            return View();
        }
        private void DeleteData()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM Deneme1";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        private void Add_Data(string marka, string fiyat, string link)

        {
            
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT Deneme1 (Marka,Fiyat,Link) VALUES (@marka,@fiyat,@link)";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@marka", marka);
            cmd.Parameters.AddWithValue("@fiyat", fiyat);
            cmd.Parameters.AddWithValue("@link",link);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
    }
}
