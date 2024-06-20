using HtmlAgilityPack;
using WebCrawler.EF;
using WebCrawler.Models;
using WebCrawler.Exceptions;
using WebCrawler.Services;
namespace WebCrawler
{
    public class Crawler
    {
        private HttpClient Client { get; set; }
        public string FilePathVisitados { get; set; }
        Repository Repository { get; set; }


        public Crawler(HttpClient client, string filePathVisitados, Repository repository)
        {
            Client = client;
            FilePathVisitados = filePathVisitados;
            Repository = repository;
        }

        public async Task<string> FazerRequisicao(string url)
        {

            try
            {
                HttpResponseMessage response =  await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responsebody = await response.Content.ReadAsStringAsync();
                return responsebody;
            }
            catch(Exception ex) 
            {
                await CrawlerExceptions.Exceptions(ex);
                return string.Empty;
            }
        }

        public async Task Executar(List<string> urls, string informacaoProcurada)
        {
            foreach (var url in urls)
            {
                int tentativas = 0;
                while (tentativas < 3)
                {
                    if (LinkService.VerificarSeOLinkEProibido(url))
                    {
                        break;
                    }
                    if(!File.Exists(FilePathVisitados)) 
                    {
                        ArquivoService.CriarNovoArquivo(FilePathVisitados);
                    }
                    if (ArquivoService.VerificarSeTemOConteudoNoAquivo(url,FilePathVisitados).Result)
                    {
                        Console.WriteLine("Site já visitado: " + url);
                        break;
                    }

                    var html = await FazerRequisicao(url);

                    if (string.IsNullOrEmpty(html))
                    {
                        Console.WriteLine("Erro ao obter o HTML de: " + url);
                        tentativas++;
                        break;
                    }

                    if (html.Contains(informacaoProcurada))
                    {
                        Console.WriteLine($"Informação encontrada em {url}: {informacaoProcurada}");
                    }

                    await Repository.Create(new Site(url, html,informacaoProcurada));

                    await ArquivoService.GravarNoArquivoAsync(url,FilePathVisitados);

                    var links = await LinkService.ExtractLinksAsync(html, url);
                    if (links.Length == 0)
                    {
                        Console.WriteLine("Nenhum link encontrado em: " + url);
                        break;
                    }

                    
                    var linksAleatorios = LinkService.EscolherLinksAleatorios(links.ToList());
                    await Executar(linksAleatorios, informacaoProcurada);

                    break; 
                }
            }
        }
        
    }
}
