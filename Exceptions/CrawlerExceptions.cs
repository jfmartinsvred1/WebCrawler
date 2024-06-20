using WebCrawler.Services;

namespace WebCrawler.Exceptions
{
    public class CrawlerExceptions
    {
        public static async Task Exceptions(Exception ex)
        {
            var logService = new LogService();
            switch (ex.GetType().Name)
            {
                //case "HttpRequestException":
                //    await logService.GerarErrorLog("[Erro] no servidor destino...");
                //    break;
                default:
                    await logService.GerarErrorLog($"[Erro] - {DateTime.Now.ToString("hh-mm")} - {ex.GetType().Name}");
                    break;
            }
        }

    }
}
