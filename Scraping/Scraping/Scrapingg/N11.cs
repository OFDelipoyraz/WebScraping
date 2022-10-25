using HtmlAgilityPack;
using Scraping.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraping.Scrapingg
{
    public class N11
    {
        List<Computers> pc = new List<Computers>();
        static ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
        public List<Computers> n11()
        {
            return Scrap_n11();
        }
        public List<Computers> Scrap_n11()
        {
            List<String> URLLER = new List<String>();
            List<String> Fiyatlar = new List<String>();
            var url = "";
            var para = "";
            for (int k = 1; k <3;k++)
            {
                var sitehtml = "https://www.n11.com/arama?q=notebook=";
                var html = GetHtml(sitehtml + k);
                var links = html.CssSelect("div.columnContent");
                foreach (var link in links)
                {
                    try
                    {
                        var mainFiyat = link.CssSelect("ins");
                        foreach (var lin1k in mainFiyat)
                        {
                            para = lin1k.InnerHtml;
                            Fiyatlar.Add(para);
                            para = "";
                        }
                        var mainURL = link.CssSelect("a");
                        foreach (var lin1k in mainURL)
                        {
                            url = lin1k.Attributes["href"].Value;
                            if (url[8] == 'w' && url[9] == 'w' && url[10] == 'w')
                            {
                                if (url[20] == 'u' && url[21] == 'r' && url[22] == 'u' && url[23] == 'n')
                                {
                                    var kontrol = 0;
                                    for (int j = 0; j < URLLER.Count; j++)
                                    {
                                        if (lin1k.Attributes["href"].Value == URLLER[j])
                                        {
                                            kontrol++;
                                            break;
                                        }
                                    }
                                    if (kontrol == 0)
                                    {
                                        URLLER.Add(lin1k.Attributes["href"].Value);
                                    }
                                    kontrol = 0;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return Scrap_n11_ic(URLLER, Fiyatlar);
        }
        public List<Computers> Scrap_n11_ic(List<String> URLLER, List<String> Fiyatlar)
        {
            string Veri_Cekme(string veri, int deger)
            {
                var deneme = ' ';
                string ozellik = "";
                while (veri[deger] != '<')
                {
                    deneme = veri[deger];
                    ozellik = ozellik + deneme;
                    deger++;
                }
                return ozellik;
            }
            string Veri_Cekme_Ekran_Boyutu(string veri, int deger)
            {
                var deneme = ' ';
                string ozellik = "";
                while (veri[deger] != '&')
                {
                    deneme = veri[deger];
                    ozellik = ozellik + deneme;
                    deger++;
                }
                return ozellik;
            }
            string Veri_Cekme_Marka(string veri, int deger)
            {
                var deneme = ' ';
                string ozellik = "";
                while (veri[deger] != '"')
                {
                    deneme = veri[deger];
                    ozellik = ozellik + deneme;
                    deger++;
                }
                return ozellik;
            }
            for (int s = 0; s < 40; s++)
            {
                var veri = "";
                string ozellik = "";
                var fiyat = "";
                var Product = new List<Computers>();
                var sitehtml = URLLER[s];
                var html = GetHtml(sitehtml);
                var links = html.CssSelect("div.unf-prop");
                var links1 = html.CssSelect("div.unf-p-summary-right");
                Computers obj = new Computers();
                foreach (var link in links)
                {
                    try
                    {
                        var mainname = link.CssSelect("div.unf-prop-context");
                        foreach (var lin1k in mainname)
                        {
                            veri = lin1k.InnerHtml;
                        }
                        Product.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                var deneme = ' ';
                for (int k = 0; k < veri.Length; k++)
                {//İşlemci Hızı
                    if (veri[k] == 'i' && veri[k + 1] == ' ' && veri[k + 2] == 'H' && veri[k + 3] == 'ı' && veri[k + 4] == 'z' && veri[k + 5] == 'ı' && veri[k + 6] == '<')
                    {
                        k = k + 54;
                        obj.Islemci_Hizi = Veri_Cekme(veri, k);

                    }//Bellek Türü
                    else if (veri[k] == 'l' && veri[k + 1] == 'e' && veri[k + 2] == 'k' && veri[k + 3] == ' ' && veri[k + 4] == 'T' && veri[k + 5] == 'ü' && veri[k + 6] == 'r' && veri[k + 7] == 'ü' && veri[k + 8] == '<')
                    {
                        k = k + 56;
                        obj.RAM_Turu = Veri_Cekme(veri, k);
                    }//Bellek Kapasitesi
                    else if (veri[k] == 'l' && veri[k + 1] == 'e' && veri[k + 2] == 'k' && veri[k + 3] == ' ' && veri[k + 4] == 'K' && veri[k + 5] == 'a' && veri[k + 6] == 'p' && veri[k + 14] == '<')
                    {
                        k = k + 62;
                        obj.Disk_Kapasitesi = Veri_Cekme(veri, k);
                    }//İşlemci Modeli
                    else if (veri[k] == 'c' && veri[k + 1] == 'i' && veri[k + 2] == ' ' && veri[k + 3] == 'M' && veri[k + 4] == 'o' && veri[k + 5] == 'd' && veri[k + 6] == 'e' && veri[k + 7] == 'l' && veri[k + 8] == 'i' && veri[k + 9] == '<')
                    {
                        k = k + 57;
                        obj.Islemci_Modeli = Veri_Cekme(veri, k);
                    }//İşlemci
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 4] == 'm' && veri[k + 5] == 'c' && veri[k + 6] == 'i' && veri[k + 7] == '<')
                    {
                        k = k + 55;
                        obj.Islemci = Veri_Cekme(veri, k);
                    }//Disk Kapasitesi
                    else if (veri[k] == 'i' && veri[k + 1] == 's' && veri[k + 2] == 'k' && veri[k + 3] == ' ' && veri[k + 4] == 'K' && veri[k + 5] == 'a' && veri[k + 6] == 'p' && veri[k + 14] == '<')
                    {
                        k = k + 62;
                        obj.RAM = Veri_Cekme(veri, k);
                    }//Model
                    else if (veri[k] == 'M' && veri[k + 1] == 'o' && veri[k + 2] == 'd' && veri[k + 3] == 'e' && veri[k + 4] == 'l' && veri[k + 5] == '<')
                    {
                        k = k + 53;
                        obj.Model = Veri_Cekme(veri, k);
                    }//İşletim Sistemi
                    else if (veri[k] == 't' && veri[k + 1] == 'i' && veri[k + 2] == 'm' && veri[k + 3] == ' ' && veri[k + 4] == 'S' && veri[k + 5] == 'i' && veri[k + 6] == 's' && veri[k + 11] == '<')
                    {
                        k = k + 59;
                        obj.Isletim_Sistemi = Veri_Cekme(veri, k);
                    }//Ağırlık
                    else if (veri[k] == 'A' && veri[k + 1] == 'ğ' && veri[k + 2] == 'ı' && veri[k + 3] == 'r' && veri[k + 4] == 'l' && veri[k + 5] == 'ı' && veri[k + 6] == 'k' && veri[k + 7] == '<')
                    {
                        k = k + 55;
                        obj.Agirlik = Veri_Cekme(veri, k);
                    }//Ekran Çözünürlüğü
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 3] == 'a' && veri[k + 4] == 'n' && veri[k + 5] == ' ' && veri[k + 6] == 'Ç' && veri[k + 7] == 'ö' && veri[k + 17] == '<')
                    {
                        k = k + 65;
                        obj.Cozunurluk = Veri_Cekme(veri, k);
                    }//Ekran Boyutu
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 3] == 'a' && veri[k + 4] == 'n' && veri[k + 5] == ' ' && veri[k + 6] == 'B' && veri[k + 7] == 'o' && veri[k + 12] == '<')
                    {
                        k = k + 60;
                        obj.Ekran_Boyutu = Veri_Cekme_Ekran_Boyutu(veri, k);
                    }//markalar/Dell"
                    else if (veri[k] == 'm' && veri[k + 1] == 'a' && veri[k + 2] == 'r' && veri[k + 3] == 'k' && veri[k + 4] == 'a' && veri[k + 5] == 'l' && veri[k + 6] == 'a' && veri[k + 7] == 'r' && veri[k + 8] == '/')
                    {
                        k = k + 9;
                        obj.Marka = Veri_Cekme_Marka(veri, k);
                    }
                    obj.Link = URLLER[s];
                    obj.Fiyat = Fiyatlar[s];
                    obj.Site_Id = 1;
                }
                pc.Add(obj);
            }
            return pc;

        }
        public static HtmlNode GetHtml(string URL)
        {
            _scrapBrowser.IgnoreCookies = true;
            _scrapBrowser.Timeout = TimeSpan.FromMinutes(15);
            _scrapBrowser.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";
            WebPage _webpage = _scrapBrowser.NavigateToPage(new Uri(URL));
            return _webpage.Html;
        }
    }
}
