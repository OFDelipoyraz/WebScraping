using MongoDB.Driver;
using MongoDB.Bson;
using Scraping.Models;

namespace Scraping.Database
{
    public class MongoDBContext
    {       private readonly IMongoCollection<Computers> computersCollection;
            public MongoDBContext()
            {
                
                MongoClient client = new MongoClient("mongodb+srv://delipoyraz:192837465@cluster0.eb7co48.mongodb.net/test");
                IMongoDatabase database = client.GetDatabase("WebScraping");
                computersCollection = database.GetCollection<Computers>("Computers");
            }
    }
}
