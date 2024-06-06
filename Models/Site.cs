using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Models
{
    public class Site
    {
        public Site(string url, string informacoes)
        {
            Url = url;
            Informacoes = informacoes;
        }

        public string Url { get; set; }
        public string Informacoes { get; set; }
    }
}
