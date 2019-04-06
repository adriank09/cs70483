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

        static void TaskCancellationDemo()
        {
            // CancellationToken demo
            Task.Run(() => ContinuousTick());

            System.Console.WriteLine("Press any key to stop the clock.");
            System.Console.ReadKey();
            cancellationTokenSource.Cancel();

            System.Console.WriteLine("Clock stopped.");
            System.Console.ReadKey();
        }

        delegate int GetValue();
        static GetValue getLocalInt;
        delegate int IntOperation(int a, int b);

        static void Main(string[] args)
        {
            // local variable set to 99
            int localInt = 99;

            // set delegate getLocalInt to a lambda expression that returns the value of localInt
            getLocalInt = () => localInt;

            System.Console.WriteLine(getLocalInt());

            //////////////////////////////////////
            // uses the delegate IntOperation
            IntOperation add = (int x, int y) =>  x+y;
            System.Console.WriteLine(add(2,3));

            IntOperation sub = (int a, int b) => a-b;
            System.Console.WriteLine(sub(2,3));
            //////////////////////////////////////

            //////////////////////////////////////
            //  uses the Func built in delegate
            Func<int, int, int> mul = (int a, int b) => a*b;
            System.Console.WriteLine(mul(2,3));


            Action<string> logMessage = (string message) => 
                System.Console.WriteLine("Message logged: {0}", message);

            logMessage(mul(5,5).ToString());

            Predicate<int> dividesByThree = (int i) =>
                i % 3 == 0;

            System.Console.WriteLine(dividesByThree(3));
            System.Console.WriteLine(dividesByThree(5));
        }

        static void AssignCountry(object sender, PersonEventArgs e)
        {
            System.Console.WriteLine("Person assigned country.");
            System.Console.WriteLine("Person country: {0}", e.Country);
        }
        static void AgeChanged(object sender, EventArgs e)
        {
            if(sender is Person)
            {
                var p = sender as Person;


            }
        }
    }
}
