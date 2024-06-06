using HtmlAgilityPack;
using WebCrawler.EF;
using WebCrawler.Models;
namespace WebCrawler
{
    public class Crawler
    {
        private HttpClient Client { get; set; }
        Repository Repository { get; set; }

        public Crawler(HttpClient httpClient,Repository repository)
        {
            Client = httpClient;
            Repository = repository;
        }

        public string ExtrairInformacoes(string html)
        {
            return $"Informações encontradas: {html[..100]}";
        }

        public async Task<string> FazerRequisicao(string url)
        {
            Client.Timeout = TimeSpan.FromSeconds(10);
            try
            {
                HttpResponseMessage response =  Client.GetAsync(url);
                return response.Content.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public string[] ExtractLinks(string html, string baseUrl)
        {
            var links = new List<string>();
            var web = new HtmlDocument();
            web.LoadHtml(html);

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
                        catch (UriFormatException e)
                        {
                            Console.WriteLine("Erro ao processar URL: " + href);
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            return links.ToArray();
        }
        public async Task Executar(List<string> urls, string informacaoProcurada)
        {
            GerenciadorDeVisitados gerenciador = new GerenciadorDeVisitados();


            foreach (var url in urls)
            {
                int tentativas = 0;
                while (tentativas < 3)
                {
                    if (gerenciador.VerificarSeJaFoiVisitado(url).Result)
                    {
                        Console.WriteLine("Site já visitado: " + url);
                        break;
                    }

                    Console.WriteLine("Visitando: " + url);
                    var html = await FazerRequisicao(url);

                    if (string.IsNullOrEmpty(html))
                    {
                        Console.WriteLine("Erro ao obter o HTML de: " + url);
                        tentativas++;
                        continue;
                    }

                    var informacoes = ExtrairInformacoes(html);
                    if (informacoes.Contains(informacaoProcurada))
                    {
                        Console.WriteLine($"Informação encontrada em {url}: {informacaoProcurada}");
                    }

                    Repository.Create(new Site(url, informacoes));
                    gerenciador.MarcarComoVisitadoAsync(url);

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

