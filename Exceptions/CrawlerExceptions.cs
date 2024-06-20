using WebCrawler.Services;

namespace WebCrawler.Exceptions
{
    public class CrawlerExceptions
    {
        public static async Task Exceptions(Exception ex)
        {
            var logService = new LogService();

            await logService.GerarErrorLog($"[Erro] - {DateTime.Now.ToString("hh-mm")} - {ex.GetType().Name}");

        }

    }
}
