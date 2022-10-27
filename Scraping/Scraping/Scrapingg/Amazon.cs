using HtmlAgilityPack;
using Scraping.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraping.Scrapingg
{
    public class Amazon
    {
        List<Computers> pc = new List<Computers>();
        static ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
        public List<Computers> amazon()
        {
            return Scrap_amazon();
        }
        public List<Computers> Scrap_amazon()
        {
            var url = "";
            var para = "";
            List<String> URLLER = new List<String>();
            List<String> Fiyatlar = new List<String>();
            for (int k = 1; k < 11; k++)
            {
                var sitehtml = "https://www.amazon.com.tr/s?i=computers&rh=n%3A12601898031&fs=true&page=";
                var html = GetHtml(sitehtml + k);
                var links = html.CssSelect("div.a-spacing-base");
                foreach (var link in links)
                {
                    try
                    {
                        var mainURL = link.CssSelect("a.s-no-outline");
                        foreach (var lin1k in mainURL)
                        {
                            url = lin1k.Attributes["href"].Value;
                            if (url[1] == 's' && url[2] == 's' && url[3] == 'p' && url[4] == 'a' && url[5] == '/')
                            {

                            }
                            else
                            {
                                URLLER.Add(lin1k.Attributes["href"].Value);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
           return Scrap_amazon_ic(URLLER);
        }
        public List<Computers> Scrap_amazon_ic(List<String> URLLER)
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
            for (int s = 0; s < 200; s++)
            {
                var veri = "";
                string ozellik = "";
                var fiyat = "";
                var Product = new List<Computers>();
                var sitehtml = "https://www.amazon.com.tr" + URLLER[s];
                var html = GetHtml(sitehtml);
                var links = html.CssSelect("div.a-expander-inline-container");//a-offscreen
                var links1 = html.CssSelect("div.aok-align-center");
                var links2 = html.CssSelect("i.a-star-5");
                Computers PC = new Computers();
                foreach (var link in links1)// Fiyat bulma
                {
                    try
                    {
                        var mainFiyat = link.CssSelect("span.a-offscreen");
                        foreach (var lin1k in mainFiyat)
                        {
                            fiyat = lin1k.InnerHtml;
                            PC.Fiyat = fiyat;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                foreach (var link in links2)// Puan bulma
                {
                    try
                    {
                        var mainFiyat = link.CssSelect("span.a-icon-alt");
                        foreach (var lin1k in mainFiyat)
                        {
                            var tut = "";
                            if (lin1k.InnerHtml.Length == 22)
                            {
                                tut += lin1k.InnerHtml[19];
                                tut += lin1k.InnerHtml[20];
                                tut += lin1k.InnerHtml[21];
                                PC.Puan = tut;
                                tut = "";
                            }
                            else
                            {
                                PC.Puan = "0,0";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                foreach (var link in links)
                {
                    try
                    {
                        var mainname = link.CssSelect("div.a-section-expander-inner");
                        foreach (var lin1k in mainname)
                        {
                            veri = lin1k.InnerHtml;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                var deneme = ' ';
                for (int k = 0; k < veri.Length; k++)
                {//Marka
                    if (veri[k] == 'M' && veri[k + 1] == 'a' && veri[k + 2] == 'r' && veri[k + 3] == 'k' && veri[k + 4] == 'a' && veri[k + 5] == ' ' && veri[k + 6] == '<')
                    {
                        k = k + 77;
                        PC.Marka = Veri_Cekme(veri, k);
                    }//Seri
                    else if (veri[k] == 'S' && veri[k + 1] == 'e' && veri[k + 2] == 'r' && veri[k + 3] == 'i' && veri[k + 4] == ' ' && veri[k + 5] == '<')
                    {
                        k = k + 76;
                        PC.Model = Veri_Cekme(veri, k);
                    }//Ekran Boyutu
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 3] == 'a' && veri[k + 4] == 'n' && veri[k + 5] == ' ' && veri[k + 6] == 'B' && veri[k + 7] == 'o' && veri[k + 13] == '<')
                    {
                        k = k + 84;
                        PC.Ekran_Boyutu = Veri_Cekme(veri, k);
                    }//Çözünürlük
                    else if (veri[k] == 'Ç' && veri[k + 1] == 'ö' && veri[k + 2] == 'z' && veri[k + 3] == 'ü' && veri[k + 4] == 'n' && veri[k + 5] == 'ü' && veri[k + 6] == 'r' && veri[k + 7] == 'l' && veri[k + 11] == '<')
                    {
                        k = k + 82;
                        PC.Cozunurluk = Veri_Cekme(veri, k);
                    }//İşletim Sistemi
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 7] == ' ' && veri[k + 8] == 'S' && veri[k + 9] == 'i' && veri[k + 10] == 's' && veri[k + 16] == '<')
                    {
                        k = k + 87;
                        PC.Isletim_Sistemi = Veri_Cekme(veri, k);
                    }//Sabit Sürücü Boyutu
                    else if (veri[k] == 'S' && veri[k + 1] == 'a' && veri[k + 2] == 'b' && veri[k + 5] == ' ' && veri[k + 6] == 'S' && veri[k + 7] == 'ü' && veri[k + 8] == 'r' && veri[k + 12] == ' ' && veri[k + 13] == 'B' && veri[k + 14] == 'o' && veri[k + 20] == '<')
                    {
                        k = k + 91;
                        PC.Disk_Kapasitesi = Veri_Cekme(veri, k);
                    }//RAM Boyutu
                    else if (veri[k] == 'R' && veri[k + 1] == 'A' && veri[k + 2] == 'M' && veri[k + 3] == ' ' && veri[k + 4] == 'B' && veri[k + 5] == 'o' && veri[k + 6] == 'y' && veri[k + 11] == '<')
                    {
                        k = k + 82;
                        PC.RAM = Veri_Cekme(veri, k);
                    }//İşlemci Markası
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 4] == 'm' && veri[k + 7] == ' ' && veri[k + 8] == 'M' && veri[k + 9] == 'a' && veri[k + 10] == 'r' && veri[k + 16] == '<')
                    {
                        k = k + 87;
                        PC.Islemci = Veri_Cekme(veri, k);
                    }//İşlemci Türü
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 4] == 'm' && veri[k + 7] == ' ' && veri[k + 8] == 'T' && veri[k + 9] == 'ü' && veri[k + 10] == 'r' && veri[k + 13] == '<')
                    {
                        k = k + 84;
                        PC.Islemci_Modeli = Veri_Cekme(veri, k);
                    }//Ürün Ağırlığı
                    else if (veri[k] == 'Ü' && veri[k + 1] == 'r' && veri[k + 2] == 'ü' && veri[k + 3] == 'n' && veri[k + 4] == ' ' && veri[k + 5] == 'A' && veri[k + 6] == 'ğ' && veri[k + 7] == 'ı' && veri[k + 8] == 'r' && veri[k + 14] == '<')
                    {
                        k = k + 85;
                        PC.Agirlik = Veri_Cekme(veri, k);
                    }//Bellek Teknolojisi
                    else if (veri[k] == 'B' && veri[k + 1] == 'e' && veri[k + 2] == 'l' && veri[k + 6] == ' ' && veri[k + 7] == 'T' && veri[k + 8] == 'e' && veri[k + 9] == 'k' && veri[k + 19] == '<')
                    {
                        k = k + 90;
                        PC.RAM_Turu = Veri_Cekme(veri, k);
                    }//İşlemci Hızı
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 4] == 'm' && veri[k + 7] == ' ' && veri[k + 8] == 'H' && veri[k + 9] == 'ı' && veri[k + 10] == 'z' && veri[k + 13] == '<')
                    {
                        k = k + 84;
                        PC.Islemci_Hizi = Veri_Cekme(veri, k);
                    }
                    PC.Link = "https://www.amazon.com.tr"+ URLLER[s];
                    PC.Site_Id = 2;
                }
                pc.Add(PC);
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
