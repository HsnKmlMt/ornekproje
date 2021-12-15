using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using ornekproje.DataAccess;

namespace ornekproje
{
    class Program
    {
        async static Task Main(string[] args)
        {
            DataAdaptor dataAdaptor = new DataAdaptor();

            var data = dataAdaptor.GetAdvertisementNamesAsync();

            for (int i = 0; i < data.Count(); i++)
            {
                Console.WriteLine($"{i}) {data[i].AdvertisementName}");
            }
            //var hrefs = result.Select(a => a.GetAttributeValue("href", string.Empty));
            while (true)
            {
                Console.WriteLine("Lütfen detayını görmek istediğiniz ilan numarasını yazınız: ");
                string answer = Console.ReadLine();
                bool isCorrect = Int32.TryParse(answer, out int parsedNumber);

                int selectedNumber = parsedNumber+1;

                if (isCorrect)
                {
                    Console.WriteLine(data[selectedNumber].Price);
                }

                else
                {
                    Console.WriteLine("Lütfen bir sayı giriniz");
                }
            }

        }
        
    }
}
