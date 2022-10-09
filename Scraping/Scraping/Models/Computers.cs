using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scraping.Models
{
    public class Computers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Fiyat { get; set; }
        public string Link { get; set; }


        public Computers(int id, String marka, String fiyat, String link)
        {
            Id = id;
            Marka = marka;
            Fiyat = fiyat;
            Link = link;

        }

    }


}
