using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class GerenciadorDeVisitados
    {
        private readonly string filePath = "C:\\Users\\Joao\\Desktop\\Projetos\\WebCrawler\\WebCrawler\\visitados.txt";
        public GerenciadorDeVisitados()
        {
            
        }
        public async Task MarcarComoVisitadoAsync(string url)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    await sw.WriteLineAsync(url);
                }

                Console.WriteLine("Linha adicionada com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro.");
                Console.WriteLine(e.Message);
            }
        }
        public async Task<bool> VerificarSeJaFoiVisitado(string url)
        {
            bool found = false;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        lineNumber++;
                        if (line.Contains(url))
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
                Console.WriteLine("Ocorreu um erro.");
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}
