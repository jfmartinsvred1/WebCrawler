using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebCrawler.Models;
namespace WebCrawler.EF
{
    public class WebCrawlerContext : DbContext
    {
        private string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = WebCrawler; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        public DbSet<Site> Sites { get; set; }
        public WebCrawlerContext()
        {
        }
        public WebCrawlerContext(IConfiguration configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
