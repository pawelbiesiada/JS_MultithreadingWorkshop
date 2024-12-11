using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Async
{
    public class AsyncAwaitPresentation : ISample
    {
        public async void Run()
        {
            
            var cs = System.Threading.SynchronizationContext.Current;


            var value = "Pawel";
            var result = GetNameAsync(value);
            Console.WriteLine("More processing is pending.");
            ///
            ///

            ///
            ///

            await result;
            var textToDisplay =  result.Result;
            Console.WriteLine(textToDisplay);
            Console.WriteLine("Awaited for asynchronous call to complete.");

            Console.ReadKey();
        }

        private async Task<string> GetNameAsync(string myName)
        {
            //await Task.Delay(1000); //non-blocking - releases main thread to process
            //Task.Delay(1000).Wait(); //blocking, tharts new thread and wait until it's done
            Thread.Sleep(1000); // blocking, on same thread
            string result = "";
            try
            {
                var t = SayHiByNameAsync(myName).ConfigureAwait(false);
                result = await t;

                //var t2 = SayHiByName(myName);
                //var res = await t2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //throw;
            }
            Console.WriteLine("Doing some more work in async method.");
            return result;
        }

        private Task<string> SayHiByNameAsync(string name)
        {
            var task = Task.Factory.StartNew((str) => 
            {
                Console.WriteLine("\tStarted new thread.");
                //throw new Exception();
                Thread.Sleep(10000);
                return string.Format("Hi {0}!!!", str);
            }, name);           
            return task;
        }
    }
}
