using Microsoft.Extensions.Configuration;
using WebCrawler;

HttpClient client = new HttpClient();
var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
IConfiguration configuration = builder.Build();
WebCrawlerContext context = new WebCrawlerContext(configuration);

Console.WriteLine(configuration["ConnString"]);
