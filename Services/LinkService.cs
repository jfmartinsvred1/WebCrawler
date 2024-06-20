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
    }
}
