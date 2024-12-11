using System;
using System.Threading.Tasks;

namespace Multithreading.Delegates
{
    internal class BeginEndInvoke
    {
        public void Run()
        {
            var action = new Action<string>(ActionMethod);

            //action("");
            //action.Invoke("");
 
            var asyncResult = action.BeginInvoke("SomeText",
                (ar) => { Console.WriteLine("Executed on Action completed."); }, null); //IAsyncResult.AsyncState is null

            Console.WriteLine("Synchronous code running here");
            Task.Delay(10).Wait();
            action.EndInvoke(asyncResult);
            Console.WriteLine("Code executed in main method.");
            Console.ReadKey();

           
            var t = Task.Factory.FromAsync(asyncResult, action.EndInvoke);


            Console.WriteLine();
            var function = new Func<bool, string>(FunctionMethod);
            var asyncReturn = function.BeginInvoke(true, 
                (ar) => { Console.WriteLine($"Async object state parameter is {ar.AsyncState}"); }, 5);

            Console.WriteLine("Started function asynchronously");
            Task.Delay(10).Wait();
            var result = function.EndInvoke(asyncReturn);
            Console.WriteLine($"Completed function with result: {result}");
            Console.ReadKey();
        }

        private void ActionMethod(string str)
        {
            Console.WriteLine($"Asynchronously processing ActionMethod with parameter {str}");
            Task.Delay(3000).Wait();
            Console.WriteLine("Processed ActionMethod completed");
        }

        private string FunctionMethod(bool state)
        {
            Console.WriteLine($"Asynchronously processing FunctionMethod with parameter {state}");
            Task.Delay(3000).Wait();
            Console.WriteLine("Processed FunctionMethod completed");

            return "FunctionMethod result";
        }
    }
}
