using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace core_app
{
    class Program
    {
        static async Task<string> GetWebPageAsync(string url)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

        static async Task<IEnumerable<string>> GetWebPagesAsync(string[] urls)
        {
            var tasks = new List<Task<string>>();

            // Loop through each URL
            foreach(string url in urls)
            {
                tasks.Add(GetWebPageAsync(url));
            }

            return await Task.WhenAll(tasks);
        }
        
        static void Main(string[] args)
        {
            // var webPage = GetWebPageAsync("https://www.newinti.edu.my").Result;
            // System.Console.WriteLine(webPage);

            var multipleWebPages = GetWebPagesAsync(new string[] {
                "https://www.newinti.edu.my",
                "https://www.adriankhor.my"
            }).Result;

            foreach(var result in multipleWebPages)
            {
                System.Console.WriteLine(result);
                System.Console.WriteLine();
            }
        }
    }
}
