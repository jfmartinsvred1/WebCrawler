using Microsoft.Extensions.Configuration;
using WebCrawler;
using WebCrawler.EF;

HttpClient client = new HttpClient();
var visitados = "C:\\Users\\Joao\\Desktop\\Projetos\\WebCrawler\\WebCrawler\\visitados.txt";
var logs = "C:\\Users\\Joao\\Desktop\\Projetos\\WebCrawler\\WebCrawler\\log.txt";


client.Timeout = TimeSpan.FromSeconds(10);
var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
IConfiguration configuration = builder.Build();
WebCrawlerContext context = new WebCrawlerContext();
Repository repository = new Repository(context);



string[] urlsIniciais = ["https://www.alura.com.br/apostila-csharp-orientacao-objetos/o-que-e-c-e-net",
    "https://blog.somostera.com/desenvolvimento-web/linguagem-c", "https://dotnet.microsoft.com/pt-br/languages/csharp",
    "https://www.alura.com.br/artigos/csharp-linguagem-programacao-dotnet",
    "https://balta.io/cursos/fundamentos-csharp",
    "https://www.googleadservices.com/pagead/aclk?sa=L&ai=DChcSEwi60JTJ6uKGAxWqA60GHYt3BwoYABACGgJwdg&ase=2&gclid=Cj0KCQjwvb-zBhCmARIsAAfUI2ux5X6Xi_40tOPICVs4a3-3PDQRr87zG_Fh6p_0SFsTSFpwfXkOuCUaAtyhEALw_wcB&ohost=www.google.com&cid=CAESVuD2xhf2V8SzzrnLq4dNf6ghxqLJWvHJkx8PTB3Gg3GRlkSTjhUjVQx_H57opGj9tARYp6AzYkUyF-cCbzOeJjku1sFVaHj9TLbhJF03ilZsr2JqpmmO&sig=AOD64_0b3-w9I7ZXaW5Ck0iuH5vqCt-1Bw&q&nis=4&adurl&ved=2ahUKEwiJ_IrJ6uKGAxU_pZUCHUCgBdg4ChDRDHoECAIQAQ"
];
string informacao = "C#";
Console.WriteLine($"Informação A Ser Procurada: {informacao}");
Crawler crawler = new Crawler(client,visitados,repository);

await crawler.Executar(urlsIniciais.ToList(),informacao);
