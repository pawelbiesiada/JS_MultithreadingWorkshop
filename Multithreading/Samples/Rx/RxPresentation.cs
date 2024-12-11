using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading.Tasks;
using System.IO;

namespace Multithreading.Samples.Rx
{
    internal class RxPresentation : ISample
    {
        public void Run()
        {
            Console.WriteLine("Current thread Id: {0}", Thread.CurrentThread.ManagedThreadId);

            //var observable = "Hello world!".ToObservable();
            //observable.Subscribe(Console.WriteLine);
            var scheduler = Observable.Interval(TimeSpan.FromSeconds(1))
                .Timestamp()
                .Where(t => t.Value % 2 == 0)              
                .Select(t => t.Timestamp)
                .Subscribe(t => {
                    Console.Write("Current thread Id: {0} ", Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("TimeStamp: {0}", t.ToLocalTime());
                });

            scheduler.Dispose();

            try
            {
                var printer = Directory.GetFiles(@"C:\Temp")

                    .ToObservable()

                    .Where(path => path.EndsWith("txt"))
                    .Retry(3)
                    .Throttle(TimeSpan.FromSeconds(60))                    
                    .Subscribe(path =>
                    {
                        //throw new Exception();
                        Console.WriteLine($"File name: {path}. Size: {new FileInfo(path).Length / 1024} KB ");
                    },
                    (ex) => { });


            }
            catch (Exception)
            {

                throw;
            }

            ///
            ///


            Console.WriteLine("Press any key to stop scheduler.\n");
            Console.ReadKey();
            scheduler.Dispose();
            Console.ReadKey();

            Console.WriteLine("Current thread Id: {0}", Thread.CurrentThread.ManagedThreadId);


            //var range = Observable.Range(0, 9);
            //range.Subscribe(
            //    (i) =>
            //    {
            //        Task.Delay(300).Wait();
            //        Console.WriteLine("{0}\t{1}", Thread.CurrentThread.ManagedThreadId, i);
            //    },   //OnNext
            //    (ex) => { Console.WriteLine(ex.Message); },                 //OnError
            //    () => { Console.WriteLine("Processing completed."); }        //OnComplete
            //);


            //var action = GetNameAsync("Paweł").ToObservable();
            //action.Subscribe(
            //    (i) =>
            //    {
            //        Task.Delay(300).Wait();
            //        Console.WriteLine("{0}\t{1}", Thread.CurrentThread.ManagedThreadId, i);
            //    },   //OnNext
            //    (ex) => { Console.WriteLine("OnError : " + ex.Message); },                 //OnError
            //    () => { Console.WriteLine("Processing completed."); }        //OnComplete
            //);
            Console.ReadKey();
        }

        private async Task<string> GetNameAsync(string myName)
        {
            string result = "";
            var t = Task.Factory.StartNew((str) => { return SayHiByName(str.ToString()); }, myName);
            result = await t;

            Console.WriteLine("Doing some more work in async method on thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        private string SayHiByName(string name)
        {
            Task.Delay(3000).Wait();
            return string.Format("Hi {0}!!!", name);
        }
    }
}
