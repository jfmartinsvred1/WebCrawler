using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class GerenciadorDeVisitados
    {
        public string FilePath { get; set; }
        private readonly ILog log = LogManager.GetLogger(typeof(GerenciadorDeVisitados));
        

        public GerenciadorDeVisitados(string filePath)
        {
            FilePath = filePath;
        }

        public async Task MarcarComoVisitadoAsync(string url)
        {
            BasicConfigurator.Configure();
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    await sw.WriteLineAsync(url);
                }

                Console.WriteLine("Linha adicionada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao marcar como visitado " + ex.Message);
            }
        }
        public async Task<bool> VerificarSeJaFoiVisitado(string url)
        {
            bool found = false;
            BasicConfigurator.Configure();
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string? line;
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
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao verificar se ja foi visitado " + ex.Message);
                return false;
            }

        }
    }
}