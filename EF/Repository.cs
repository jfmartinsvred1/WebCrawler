using WebCrawler.Models;

namespace WebCrawler.EF
{
    public class Repository
    {
        public WebCrawlerContext Context { get; set; }

        public Repository(WebCrawlerContext context)
        {
            Context = context;
        }


        public async Task Create(Site site)
        {
            try
            {
                Context.Sites.AddAsync(site);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
