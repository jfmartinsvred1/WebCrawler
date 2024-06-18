using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Models
{
    public class Site
    {
        public Site(string url, string informacoes, string assunto)
        {
            Url = url;
            Informacoes = informacoes;
            Assunto = assunto;
        }
        [Key]
        public string Key { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; }
        public string Assunto { get; set; }
        public string Informacoes { get; set; }
    }
}
