using HtmlAgilityPack;
using WebCrawler.Exceptions;

namespace WebCrawler.Services
{
    public class LinkService
    {
        public static List<string> EscolherLinksAleatorios(List<string> links)
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
        public static async Task<string[]> ExtractLinksAsync(string html, string baseUrl)
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
                        catch (Exception ex)
                        {
                            await CrawlerExceptions.Exceptions(ex);

                        }
                    }
                }
            }
            return links.ToArray();
        }

        public static bool VerificarSeOLinkEProibido(string url)
        {
            List<string> linksNaoPermitidos = new List<string>();
            linksNaoPermitidos.Add("https://l.facebook.com");
            bool response = false;
            foreach(var link in linksNaoPermitidos)
            {
                if (url.Contains(link))
                {
                    response = true;
                }
            }
            return response;    

        }
    }
}
