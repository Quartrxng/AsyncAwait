using System.Net.Http;

namespace Parser
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите сайты, которые хотите с парсить. Чтобы остановиться напишите stop.");
            var sites = new List<string>();
            while (true)
            {
                string n = Console.ReadLine();
                if (n == "stop")
                {
                    break;
                }
                else if ( string.IsNullOrEmpty(n) || string.IsNullOrWhiteSpace(n))
                {
                    continue;
                }
                sites.Add(n);
            }
            string[] pages = await Parser(sites);
            foreach (string page in pages) 
            { 
                Console.WriteLine(page);
            }

        }
        static async Task<string[]> Parser(List<string> sites)
        {
            var httpClient = new HttpClient();
            Task<string>[] downloadTasks = sites.Select(url => httpClient.GetStringAsync(url)).ToArray();
            return await Task.WhenAll(downloadTasks);
        }
    }
}
