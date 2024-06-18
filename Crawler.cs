using HtmlAgilityPack;
using WebCrawler.EF;
using WebCrawler.Models;
using log4net;
using log4net.Config;
using System.Net;
using WebCrawler.Exceptions;
namespace WebCrawler
{
    public class Crawler
    {
        private HttpClient Client { get; set; }
        public string FilePathVisitados { get; set; }
        Repository Repository { get; set; }
        private readonly static ILog log = LogManager.GetLogger(typeof(Crawler));


        public Crawler(HttpClient client, string filePathVisitados, Repository repository)
        {
            Client = client;
            FilePathVisitados = filePathVisitados;
            Repository = repository;
        }

        public string ExtrairInformacoes(string html)
        {
            BasicConfigurator.Configure();
            return $"Informações encontradas: {html[..100]}";
        }

        public async Task<string> FazerRequisicao(string url)
        {
            BasicConfigurator.Configure();

            try
            {
                HttpResponseMessage response =  await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responsebody = await response.Content.ReadAsStringAsync();
                return responsebody;
            }
            catch(Exception ex) 
            {
                CrawlerExceptions.Exceptions(ex);
                return string.Empty;
            }
        }

        public string[] ExtractLinks(string html, string baseUrl)
        {
            var links = new List<string>();
            var web = new HtmlDocument();
            web.LoadHtml(html);
            BasicConfigurator.Configure();

            var anchorTags = web.DocumentNode.SelectNodes("//a[@href]");
            if (anchorTags != null)
            {
                foreach (var tag in anchorTags)
                {
                    string href = tag.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(href) && !href.StartsWith("#"))
                    {
                        try
                        {
                            Uri baseUri = new Uri(baseUrl);
                            Uri absoluteUri = new Uri(baseUri, href);
                            links.Add(absoluteUri.ToString());
                        }
                        catch (Exception ex)
                        {
                            CrawlerExceptions.Exceptions(ex);
                            
                        }
                    }
                }
            }
            return links.ToArray();
        }
        public async Task Executar(List<string> urls, string informacaoProcurada)
        {
            GerenciadorDeVisitados gerenciador = new GerenciadorDeVisitados(FilePathVisitados);

            BasicConfigurator.Configure();
            foreach (var url in urls)
            {
                int tentativas = 0;
                while (tentativas < 3)
                {
                    if (url.Contains("https://l.facebook.com"))
                    {
                        break;
                    }
                    if (gerenciador.VerificarSeJaFoiVisitado(url).Result)
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

                    var informacoes = ExtrairInformacoes(html);
                    if (informacoes.Contains(informacaoProcurada))
                    {
                        Console.WriteLine($"Informação encontrada em {url}: {informacaoProcurada}");
                    }

                    await Repository.Create(new Site(url, informacoes,informacaoProcurada));
                    await gerenciador.MarcarComoVisitadoAsync(url);

                    var links = ExtractLinks(html, url);
                    if (links.Length == 0)
                    {
                        Console.WriteLine("Nenhum link encontrado em: " + url);
                        break;
                    }

                    
                    var linksAleatorios = EscolherLinksAleatorios(links.ToList());
                    await Executar(linksAleatorios, informacaoProcurada);

                    break; 
                }
            }
        }
        static List<string> EscolherLinksAleatorios(List<string> links)
        {
            int numLinksParaEscolher = Math.Min(5, links.Count);
            var linksAleatorios = new List<string>();
            var random = new Random();
            BasicConfigurator.Configure();
            for (int i = 0; i < numLinksParaEscolher; i++)
            {
                int indiceAleatorio = random.Next(links.Count);
                linksAleatorios.Add(links[indiceAleatorio]);
                links.RemoveAt(indiceAleatorio); 
            }

            return linksAleatorios;
        }
    }
}
