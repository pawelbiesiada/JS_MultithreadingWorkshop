using System;
using System.Threading.Tasks;
namespace Multithreading.Samples.Tasks
{
    internal class TaskFactoryPresentation : ISample
    {
        public void Run()
        {
            var task = Task.Factory.StartNew(new Action(() => { Console.WriteLine("Run async task"); }));
            task.Wait();

            var function = new Func<object, string>((name) => { Task.Delay(3000).Wait(); return string.Format("Hi {0}!", name); });
            var functionTask = Task.Factory.StartNew<string>(function, "Pawel");
            Task.Delay(1000).Wait();
            functionTask.Wait();
            Console.WriteLine(functionTask.Result);


            //var asyncResult = function.BeginInvoke("Pawel", null, "obj");
            //Task.Delay(1000).Wait();
            //var taskFromAsync = Task.Factory.FromAsync<string>(asyncResult, function.EndInvoke );
            //taskFromAsync.Wait();
            //Console.WriteLine(taskFromAsync.Result);

            Console.ReadKey();
        }
    }
}
