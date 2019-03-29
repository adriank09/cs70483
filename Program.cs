using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace core_app
{
    class Program
    {
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

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
        
        static void BlockingCollectionDemo()
        {
            BlockingCollection<int> data = new BlockingCollection<int>(10);
            
            // data up to 10 iterations can be added
            Task.Run(() => 
            {
                for(int i=0;i<20;i++)
                {
                    data.Add(i);
                    System.Console.WriteLine("Data '{0}' added successfully.", i);
                }
                data.CompleteAdding();
            });
            System.Console.WriteLine("Reading collection...");
            Task.Run(() => 
            {
                while(!data.IsCompleted)
                {
                    try
                    {
                        int val = data.Take();
                        System.Console.WriteLine("Data '{0}' taken successfully.", val);
                    }
                    catch (System.Exception)
                    {
                        
                        throw;
                    }
                }
            });
        }

        static void ContinuousTick()
        {
            while(!cancellationTokenSource.IsCancellationRequested)
            {
                System.Console.WriteLine("Tick... {0}", DateTime.Now);
                Thread.Sleep(500);
            }
        }

        static void Main(string[] args)
        {
            object o = 100;

            switch(o)
            {
                case string a:
                    if(a.Length > 0)
                    {
                        System.Console.WriteLine(a);
                    }
                    break;
                case int i:
                    if(i % 2 == 0)
                    {
                        System.Console.WriteLine("{0} is an even number.", i);
                    }
                    break;
            }
            
            
            
            
            // CancellationToken demo
            // Task.Run(() => ContinuousTick());

            // System.Console.WriteLine("Press any key to stop the clock.");
            // System.Console.ReadKey();
            // cancellationTokenSource.Cancel();

            // System.Console.WriteLine("Clock stopped.");
            // System.Console.ReadKey();
        }
    }
}
