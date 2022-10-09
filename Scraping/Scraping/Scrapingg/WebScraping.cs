using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraping.Scrapingg
{
    public class WebScraping
    {
        
        static ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
        public List<String> Trendyol()
        {
         return   Scrap_trendyol();

        }
        public List<String> Scrap_trendyol()
        {
            List<String> URLLER = new List<String>();
            var ProductName = new List<ProductDetails>();
            for (int k = 1; k < 20; k++)
            {
                var sitehtml = "https://www.trendyol.com/laptop-x-c103108?pi=";
                var html = GetHtml(sitehtml + k);
                var links = html.CssSelect("div.card-border");
                foreach (var link in links)
                {
                    try
                    {
                        ProductDetails obj = new ProductDetails();
                        var mainname1 = link.CssSelect("span.prdct-desc-cntnr-ttl");
                        foreach (var lin1k in mainname1)
                        {
                            obj.ProductHeadbas = lin1k.InnerHtml;
                        }

                        var mainname2 = link.CssSelect("span.hasRatings");
                        foreach (var lin1k in mainname2)
                        {
                            obj.ProductHead = lin1k.InnerHtml;
                        }


                        var mainAmount = link.CssSelect("div.prc-box-dscntd");
                        foreach (var lin1k in mainAmount)
                        {
                            obj.ProductAmount = lin1k.InnerHtml;
                        }


                        var mainURL = link.CssSelect("a");
                        foreach (var lin1k in mainURL)
                        {
                            obj.ProductUrl = lin1k.Attributes["href"].Value;
                            URLLER.Add(obj.ProductUrl);
                        }

                        if (obj.ProductHead != null && obj.ProductAmount != null && obj.ProductUrl != null)
                        {
                            ProductName.Add(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return URLLER;
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
    public class ProductDetails
    {
        public string ProductHeadbas { get; set; }
        public string ProductHead { get; set; }
        public string ProductUrl { get; set; }
        public string ProductAmount { get; set; }

    }
}
