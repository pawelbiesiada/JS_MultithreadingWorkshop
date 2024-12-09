using System;
using System.Threading.Tasks;

namespace Multithreading.Samples.Tasks
{
    internal class TaskGeneric : ISample
    {
        public void Run()
        {

            //Action
            //Action<object>

            //Func<R>
            //Func<object,R>

            //Predicate<T>   <=> Func<T, bool>

            var t = new Task((obj) => { }, 2000);
            t.Start();


            Task task = Task.Run(new Action(() =>
            {
                Console.WriteLine("Run async task");
            }));

            task.Wait();
            System.Threading.Thread.Sleep(2000);
            Task<string> taskGeneric = Task.Factory.StartNew<string>(() =>
            {
                //throw new Exception();
                Task.Delay(3000).Wait();
                return "Hi!";
            });


            var name = "John Doe";
            Task<string> taskParametrized = Task.Factory.StartNew<string>((p) =>
            {
                //throw new Exception();
                Task.Delay(3000).Wait();
                return $"Hi {p}!";
            }, name);

            try
            {

                taskGeneric.Wait();

                var res = taskGeneric.Result;



            }
            catch (AggregateException axc)
            {
                Console.WriteLine(axc);
                Console.WriteLine();
                Console.WriteLine(axc.InnerException);
                //Console.WriteLine(axc.InnerExceptions); //can be used in Parrallel or PLINQ
            }

            Console.WriteLine(taskGeneric.Result);
            Console.ReadKey();
        }
    }
}
