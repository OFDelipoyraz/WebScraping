using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scraping.Models
{
    public class Computers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Marka { get; set; }

        public string Model { get; set; }
    }
}
