using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Services
{
    public class LogService
    {
        public LogService()
        {
            FileName = $"C:\\Users\\Joao\\Desktop\\Projetos\\WebCrawler\\WebCrawler\\Logs\\Log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
        }

        private readonly string FileName;
        public async Task GerarErrorLog(string mensagem)
        {
            ArquivoService.CriarNovoArquivo(FileName);
            await ArquivoService.GravarNoArquivoAsync(mensagem, FileName);
        }

    }

}
