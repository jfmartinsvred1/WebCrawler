using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebCrawler.Models;
namespace WebCrawler.EF
{
    public class WebCrawlerContext : DbContext
    {
        public string ConnectionString = "Data Source=DESKTOP-707QCVQ;Initial Catalog = WebCrawler; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        public DbSet<Site> Sites { get; set; }
        public WebCrawlerContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
