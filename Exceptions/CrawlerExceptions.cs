using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Exceptions
{
    public class CrawlerExceptions
    {
        public static void Exceptions(Exception ex)
        {
            switch (ex.GetType().Name)
            {
                case "HttpRequestException":
                    Console.WriteLine("Error no servidor destino...");
                    break;
                default:
                    Console.WriteLine("Erro "+ ex.GetType().Name);
                    break;
            }
        }

    }
}
