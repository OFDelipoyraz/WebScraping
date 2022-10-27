using HtmlAgilityPack;
using Scraping.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraping.Scrapingg
{
    public class Teknosa
    {
        List<Computers> pc = new List<Computers>();
        static ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
        public List<Computers> teknosa()
        {
            return Scrap_teknosa();
        }
        public List<Computers> Scrap_teknosa()
        {
            List<String> URLLER = new List<String>();
            List<String> Fiyatlar = new List<String>();
            var yeni = ' ';
            var yeni_para = "";
            for (int k = 1; k < 16; k++)
            {
                var sitehtml = "https://www.teknosa.com/laptop-notebook-c-116004?s=%3Arelevance&page=";

                var html = GetHtml(sitehtml + k);
                var links = html.CssSelect("div.prd");
                foreach (var link in links)
                {
                    try
                    {
                        var mainFiyat = link.CssSelect("span.prc-last");
                        foreach (var lin1k in mainFiyat)
                        {
                            for (int a = 17; a < lin1k.InnerHtml.Length; a++)
                            {
                                yeni = lin1k.InnerHtml[a];
                                yeni_para += yeni;
                            }
                            Fiyatlar.Add(yeni_para);
                            yeni_para = "";
                        }
                        var mainURL = link.CssSelect("a");
                        foreach (var lin1k in mainURL)
                        {
                            URLLER.Add(lin1k.Attributes["href"].Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return Scrap_teknosa_ic(URLLER, Fiyatlar);
        }
        public List<Computers> Scrap_teknosa_ic(List<String> URLLER, List<String> Fiyatlar)
        {

            int td_bulma(string veri, int th_sayisi, int td_sayisi, int k)
            {
                while (th_sayisi != td_sayisi)
                {
                    //td sayısı
                    if (veri[k] == '<' && veri[k + 1] == 't' && veri[k + 2] == 'd' && veri[k + 3] == '>')
                    {
                        td_sayisi++;
                    }
                    k++;
                }
                return k + 3;
            }
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
            for (int i = 0; i < 300; i++)
            {
                Computers obj = new Computers();
                var th_sayisi = 0;
                var td_sayisi = 0;
                var a = 0;
                var veri = "";
                var sitehtml = "https://www.teknosa.com" + URLLER[i];
                var html = GetHtml(sitehtml);
                var links = html.CssSelect("div.ptf-item");
                var links1 = html.CssSelect("h1.pdp-title");
                foreach (var link in links1)// marka ismi bulma
                {
                    try
                    {
                        var mainname = link.CssSelect("b");
                        foreach (var lin1k in mainname)
                        {
                            obj.Marka = lin1k.InnerHtml;
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
                        var mainname = link.CssSelect("div.ptf-body");
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
                {
                    //th sayısı
                    if (veri[k] == '<' && veri[k + 1] == 't' && veri[k + 2] == 'h' && veri[k + 3] == '>')
                    {
                        th_sayisi++;
                    }
                    //td sayısı
                    if (veri[k] == '<' && veri[k + 1] == 't' && veri[k + 2] == 'd' && veri[k + 3] == '>')
                    {
                        td_sayisi++;
                    }
                    //SSD Kapasitesi
                    if (veri[k] == 'S' && veri[k + 1] == 'S' && veri[k + 2] == 'D' && veri[k + 3] == ' ' && veri[k + 4] == 'K' && veri[k + 5] == 'a' && veri[k + 6] == 'p' && veri[k + 14] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Disk_Kapasitesi = Veri_Cekme(veri, a);

                    }//Ekran Boyutu
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 3] == 'a' && veri[k + 4] == 'n' && veri[k + 5] == ' ' && veri[k + 6] == 'B' && veri[k + 7] == 'o' && veri[k + 8] == 'y' && veri[k + 12] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Ekran_Boyutu = Veri_Cekme(veri, a);
                    }//Ekran Çözünürlüğü (Piksel)
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 3] == 'a' && veri[k + 4] == 'n' && veri[k + 5] == ' ' && veri[k + 6] == 'Ç' && veri[k + 7] == 'ö' && veri[k + 26] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Cozunurluk = Veri_Cekme(veri, a);
                    }//Model Kodu
                    else if (veri[k] == 'M' && veri[k + 1] == 'o' && veri[k + 2] == 'd' && veri[k + 3] == 'e' && veri[k + 4] == 'l' && veri[k + 5] == ' ' && veri[k + 6] == 'K' && veri[k + 7] == 'o' && veri[k + 10] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Model = Veri_Cekme(veri, a);
                    }//İşlemci
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 4] == 'm' && veri[k + 5] == 'c' && veri[k + 6] == 'i' && veri[k + 7] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Islemci = Veri_Cekme(veri, a);
                    }//İşlemci Nesli
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 7] == ' ' && veri[k + 8] == 'N' && veri[k + 9] == 'e' && veri[k + 10] == 's' && veri[k + 13] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Islemci_Modeli = Veri_Cekme(veri, a);
                    }//İşlemci Frekansı
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 7] == ' ' && veri[k + 8] == 'F' && veri[k + 9] == 'r' && veri[k + 10] == 'e' && veri[k + 16] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Islemci_Hizi = Veri_Cekme(veri, a);
                    }//İşletim Sistemi Yazılımı
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 7] == ' ' && veri[k + 8] == 'S' && veri[k + 9] == 'i' && veri[k + 15] == ' ' && veri[k + 16] == 'Y' && veri[k + 17] == 'a' && veri[k + 24] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Isletim_Sistemi = Veri_Cekme(veri, a);
                    }//Ram
                    else if (veri[k] == 'R' && veri[k + 1] == 'a' && veri[k + 2] == 'm' && veri[k + 3] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.RAM = Veri_Cekme(veri, a);
                    }//Ağırlık
                    else if (veri[k] == 'A' && veri[k + 1] == 'ğ' && veri[k + 2] == 'ı' && veri[k + 3] == 'r' && veri[k + 7] == '<')
                    {
                        k = k + 10;
                        a = td_bulma(veri, th_sayisi, td_sayisi, k);
                        obj.Agirlik = Veri_Cekme(veri, a);
                    }
                    obj.Link = "https://www.teknosa.com" + URLLER[i];
                    obj.Fiyat = Fiyatlar[i];
                    obj.Site_Id = 3;
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
