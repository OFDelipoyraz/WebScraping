using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scraping.Models
{
    public class Computers
    {
        private string v1;
        private string v2;
        private string v3;

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

        public Computers()
        {
        }

        public Computers(string v1, string v2, string v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }


}
