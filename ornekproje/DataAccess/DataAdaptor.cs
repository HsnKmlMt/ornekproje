using HtmlAgilityPack;
using ornekproje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ornekproje.DataAccess
{
    public class DataAdaptor
    {
        public Task<List<AdsModel>> GetAdvertisementNamesAsync()
        {
            //HttpClient client = new HttpClient();
            //var response =  client.GetAsync("https://www.sahibinden.com/");
            //var pageContents = response.Content.ReadAsStringAsync();
            string text = System.IO.File.ReadAllText(@"C:\Users\ashas\Desktop\homepage.txt");
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(text);



            var classes = pageDocument.DocumentNode.Descendants(0).Where(a => a.HasClass("vitrin-list")).First();

            var headlineText = pageDocument.DocumentNode.SelectNodes(classes.XPath).ToList();
            var result = headlineText[0].ChildNodes.Where(a => a.Name == "li").ToList();
            var texts = result.Select(a => a.InnerText.Replace("\n", "").Replace("\r", "").Replace("  ", "").Replace("&amp;", ""))
                .Where(a => a != "").ToList();

            var details = GetAdvertisementDetails(result);
            List<AdsModel> models = new List<AdsModel>();

            for (int i = 0; i < texts.Count; i++)
            {
                AdsModel model = new AdsModel();

                model.AdvertisementName = texts[i];
                model.Price = details[i];
                models.Add(model);
            }

            return models;
        }
        private List<string> GetAdvertisementDetails(List<HtmlNode> nodes)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\ashas\Desktop\homepage.txt");
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(text);

            List<string> prices = new List<string>();

            for (int i = 0; i < nodes.Count(); i++)
            {
                var detail = nodes[i].SelectSingleNode($"/html[1]/body[1]/div[5]/div[3]/div[1]/div[3]/div[3]/ul[1]/li[{i+1}]/a[1]");
                string link = "https://www.sahibinden.com" + detail.GetAttributeValue("href", string.Empty);
                string detailText = System.IO.File.ReadAllText(@"C:\Users\ashas\Desktop\advertisingpage.txt");
                HtmlDocument pageDocument2 = new HtmlDocument();
                pageDocument2.LoadHtml(detailText);
                var detailClasses = pageDocument2.DocumentNode.Descendants(0).Where(a => a.HasClass("classifiedInfo")).First().InnerHtml.Replace("\n", "").Replace("\r", "").
                    Replace("  ", "");
                var trim = detailClasses.LastIndexOf("TL<");
                var price = detailClasses.Substring(4, trim - 2);

                prices.Add(price);
            }
            return prices;
        }
        
    }
}
