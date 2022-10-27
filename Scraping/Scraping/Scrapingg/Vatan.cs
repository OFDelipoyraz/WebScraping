using HtmlAgilityPack;
using Scraping.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraping.Scrapingg
{
    public class Vatan
    {
        List<Computers>pc= new List<Computers>();
        static ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
        public List<Computers> vatan()
        {
            return Scrap_vatan();
        }
        public List<Computers> Scrap_vatan()
        {
            var para = "";
            var URL_link = "";
            var URL_son = "";
            List<String> URLLER = new List<String>();
            List<String> Fiyatlar = new List<String>();
            for (int k = 1; k < 19; k++)
            {
                var sitehtml = "https://www.vatanbilgisayar.com/notebook/?page=";
                var html = GetHtml(sitehtml + k);
                var links = html.CssSelect("div.product-list__content");
                foreach (var link in links)
                {
                    try
                    {
                        var mainAmount = link.CssSelect("span.product-list__price");
                        foreach (var lin1k in mainAmount)
                        {
                            para = lin1k.InnerHtml;
                            Fiyatlar.Add(para);
                        }
                        var mainURL = link.CssSelect("a");
                        foreach (var lin1k in mainURL)
                        {
                            URL_link = lin1k.Attributes["href"].Value;
                            if (URL_link[0] == '/')
                            {
                                for (int b = 0; b < URL_link.Length; b++)
                                {
                                    if (URL_link[b] == '#')
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        URL_son += URL_link[b];
                                    }
                                }
                                URLLER.Add(URL_son);
                            }
                            URL_son = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
           return Scrap_vatan_ic(URLLER);
        }
        public List<Computers> Scrap_vatan_ic(List<String> URLLER)
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
            for (int s = 0; s < 288; s++)
            {
                var veri = "";
                var isim = "";
                var marka_isim = "";
                var genel_isim = "";
                var model = "";
                var model_parcala = "";
                var Product = new List<Computers>();
                var sitehtml = "https://www.vatanbilgisayar.com/" + URLLER[s];
                var html = GetHtml(sitehtml);
                var links = html.CssSelect("div.active");
                var links1 = html.CssSelect("div.product-detail");
                Computers PC = new Computers();
                foreach (var link in links1)// genel isim ve marka ismi bulma
                {
                    try
                    {
                        var urunFiyat = link.CssSelect("span.product-list__price");//Fiyat
                        foreach (var lin1k in urunFiyat)
                        {
                            PC.Fiyat = lin1k.InnerHtml;
                        }
                        var urunModel = link.CssSelect("div.product-id");//Model
                        foreach (var lin1k in urunModel)
                        {
                            model_parcala = lin1k.InnerHtml;
                            for (int b = 1; b < model_parcala.Length; b++)
                            {
                                if (model_parcala[b] == ' ' && model_parcala[b + 1] == '/' && model_parcala[b + 2] == ' ')
                                {
                                    break;
                                }
                                else
                                {
                                    model += model_parcala[b];
                                }
                            }
                            PC.Model = model;
                            model = "";
                        }
                        var mainname = link.CssSelect("h1.product-list__product-name");//Genel Isim , Marka
                        foreach (var lin1k in mainname)
                        {
                            isim = lin1k.InnerHtml;
                            for (int b = 1; b < isim.Length; b++)
                            {
                                genel_isim += isim[b];
                            }
                            for (int b = 1; b < isim.Length; b++)
                            {
                                if (isim[b] == ' ')
                                {
                                    break;
                                }
                                else
                                {
                                    marka_isim += isim[b];
                                }
                            }
                            PC.Marka = marka_isim;
                            PC.Genel_Isim = genel_isim;
                            marka_isim = "";
                            genel_isim = "";
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
                        var mainname = link.CssSelect("div.masonry-tab");
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
                    //İşlemci Hızı
                    if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 7] == ' ' && veri[k + 8] == 'H' && veri[k + 9] == 'ı' && veri[k + 10] == 'z' && veri[k + 12] == '<')
                    {
                        k = k + 26;
                        PC.Islemci_Hizi = Veri_Cekme(veri, k);
                    }//İşlemci Markası
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 7] == ' ' && veri[k + 8] == 'M' && veri[k + 9] == 'a' && veri[k + 10] == 'r' && veri[k + 15] == '<')
                    {
                        k = k + 29;
                        PC.Islemci = Veri_Cekme(veri, k);
                    }//İşlemci Nesli
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 7] == ' ' && veri[k + 8] == 'N' && veri[k + 9] == 'e' && veri[k + 10] == 's' && veri[k + 13] == '<')
                    {
                        k = k + 27;
                        PC.Islemci_Modeli = Veri_Cekme(veri, k);
                    }//Ram (Sistem Belleği)
                    else if (veri[k] == 'R' && veri[k + 1] == 'a' && veri[k + 2] == 'm' && veri[k + 3] == ' ' && veri[k + 4] == '(' && veri[k + 5] == 'S' && veri[k + 11] == ' ' && veri[k + 12] == 'B' && veri[k + 19] == ')' && veri[k + 20] == '<')
                    {
                        k = k + 34;
                        PC.RAM = Veri_Cekme(veri, k);
                    }//Ekran Boyutu
                    else if (veri[k] == 'E' && veri[k + 1] == 'k' && veri[k + 2] == 'r' && veri[k + 5] == ' ' && veri[k + 6] == 'B' && veri[k + 7] == 'o' && veri[k + 8] == 'y' && veri[k + 12] == '<')
                    {
                        k = k + 26;
                        PC.Ekran_Boyutu = Veri_Cekme(veri, k);
                    }//Ekran Çözünürlüğü (Piksel)
                    else if (veri[k] == '(' && veri[k + 1] == 'P' && veri[k + 2] == 'i' && veri[k + 3] == 'k' && veri[k + 4] == 's' && veri[k + 5] == 'e' && veri[k + 6] == 'l' && veri[k + 7] == ')' && veri[k + 8] == '<')
                    {
                        k = k + 22;
                        PC.Cozunurluk = Veri_Cekme(veri, k);
                    }//Disk Kapasitesi
                    else if (veri[k] == 'D' && veri[k + 1] == 'i' && veri[k + 2] == 's' && veri[k + 3] == 'k' && veri[k + 4] == ' ' && veri[k + 5] == 'K' && veri[k + 6] == 'a' && veri[k + 7] == 'p' && veri[k + 15] == '<')
                    {
                        k = k + 29;
                        PC.Disk_Kapasitesi = Veri_Cekme(veri, k);
                    }//İşletim Sistemi
                    else if (veri[k] == 'İ' && veri[k + 1] == 'ş' && veri[k + 2] == 'l' && veri[k + 3] == 'e' && veri[k + 7] == ' ' && veri[k + 8] == 'S' && veri[k + 9] == 'i' && veri[k + 10] == 's' && veri[k + 11] == 't' && veri[k + 15] == '<')
                    {
                        k = k + 29;
                        PC.Isletim_Sistemi = Veri_Cekme(veri, k);
                    }//Cihaz Ağırlığı
                    else if (veri[k] == 'C' && veri[k + 1] == 'i' && veri[k + 5] == ' ' && veri[k + 6] == 'A' && veri[k + 9] == 'r' && veri[k + 10] == 'l' && veri[k + 14] == '<')
                    {
                        k = k + 28;
                        PC.Agirlik = Veri_Cekme(veri, k);
                    }
                    PC.Link = "www.vatanbilgisayar.com/" +URLLER[s];
                    PC.Site_Id = 5;
                    PC.Puan = "4";
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
