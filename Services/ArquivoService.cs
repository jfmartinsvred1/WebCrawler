using WebCrawler.Exceptions;

namespace WebCrawler.Services
{
    public static class ArquivoService
    {

        public static async Task GravarNoArquivoAsync(string conteudo, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    await sw.WriteLineAsync(conteudo);
                }
            }
            catch (Exception ex)
            {
                await CrawlerExceptions.Exceptions(ex);
            }
        }

        public static void CriarNovoArquivo(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (var sw = File.Create(filePath))
                {

                }
            }
        }
        public static void BuscaNoArquivo()
        {

        }

        public static async Task<bool> VerificarSeTemOConteudoNoAquivo(string conteudo, string filePath)
        {
            //Busca Feita Por Linha(Busca Linear)
            bool found = false;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string? line;
                    int lineNumber = 0;

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        lineNumber++;
                        if (line.Contains(conteudo))
                        {
                            Console.WriteLine($"Termo encontrado na linha {lineNumber}: {line}");
                            found = true;
                            break;
                        }
                    }
                    return found;
                }

            }
            catch (Exception e)
            {
                await CrawlerExceptions.Exceptions(e);
                return found;
            }
        }

    }
}
