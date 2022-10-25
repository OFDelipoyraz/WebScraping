using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scraping.Models
{
    public class Computers
    {

        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Fiyat { get; set; }
        public string Isletim_Sistemi { get; set; }
        public string Islemci { get; set; }
        public string Islemci_Modeli { get; set; }
        public string Islemci_Hizi { get; set; }
        public string RAM { get; set; }
        public string RAM_Turu { get; set; }
        public string Disk_Kapasitesi { get; set; }
        public string Ekran_Boyutu { get; set; }
        public string Cozunurluk { get; set; }
        public string Agirlik { get; set; }
        public int Site_Id { get; set; }
        public string Link { get; set; }
        public string Genel_Isim { get; set; }
        public string Puan { get; set; }



        public Computers()
        {
        }
        public Computers(int ıd, string marka, string model, string fiyat, string ısletim_Sistemi, string ıslemci, string ıslemci_Modeli, string ıslemci_Hizi, string rAM, string rAM_Turu, string disk_Kapasitesi, string ekran_Boyutu, string cozunurluk, string agirlik, int site_Id, string link, string genel_Isim, string puan)
        {
            Id = ıd;
            Marka = marka;
            Model = model;
            Fiyat = fiyat;
            Isletim_Sistemi = ısletim_Sistemi;
            Islemci = ıslemci;
            Islemci_Modeli = ıslemci_Modeli;
            Islemci_Hizi = ıslemci_Hizi;
            RAM = rAM;
            RAM_Turu = rAM_Turu;
            Disk_Kapasitesi = disk_Kapasitesi;
            Ekran_Boyutu = ekran_Boyutu;
            Cozunurluk = cozunurluk;
            Agirlik = agirlik;
            Site_Id = site_Id;
            Link = link;
            Genel_Isim = genel_Isim;
            Puan = puan;
        }
    }
}
