using Microsoft.Extensions.Configuration;
using WebCrawler;
using WebCrawler.EF;

HttpClient client = new HttpClient();
var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
IConfiguration configuration = builder.Build();
WebCrawlerContext context = new WebCrawlerContext(configuration);
Repository repository = new Repository(context);

string[] urlsIniciais = ["https://www.wildarte.com.br", "https://ubuntu.com", "https://canaltech.com.br", "https://techtudo.com.br", "https://www.cyberciti.biz"];
Console.WriteLine("Escreva A Informação A Ser Procurada: ");
string informacao = "ubuntu";
Crawler crawler = new Crawler(client,repository);

crawler.Executar(urlsIniciais.ToList(),informacao);
