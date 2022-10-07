using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Scraping.Database;
using Scraping.Models;

namespace Scraping.Controllers
{
    public class DataController : Controller
    {
        private MongoDBContext dBContext;
        private IMongoCollection<Computers> mongoCollection;
        public IActionResult Index()
        {

            return View();
        }
    }
}
