using System;
using System.Threading.Tasks;

namespace Multithreading.Samples.Tasks
{
    internal class TaskGeneric : ISample
    {
        public void Run()
        {

            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ////Action
            ////Action<object>

            ////Func<R>
            ////Func<object,R>

            ////Predicate<T>   <=> Func<T, bool>

            //var t = new Task((obj) => { }, 2000);
            //t.Start();


            //Task task = Task.Run(new Action(() =>
            //{
            //    Console.WriteLine("Run async task");
            //}));

            //task.Wait();
            //System.Threading.Thread.Sleep(2000);
            Task taskGeneric = Task.Factory.StartNew(() =>
            {
                throw new TimeoutException();
                Task.Delay(3000).Wait();
            });

            try
            {
                System.Threading.Thread.Sleep(1000);
                //Task.Delay(5000).Wait();

                taskGeneric.Wait();

                //var res = taskGeneric.Result;

            }
            catch (TimeoutException tex)
            {

            }
            catch (AggregateException axc)
            {                
                Console.WriteLine(axc);
                Console.WriteLine();
                Console.WriteLine(axc.InnerException);
                //Console.WriteLine(axc.InnerExceptions); //can be used in Parrallel or PLINQ
            }
            catch (Exception ex)
            {
            }

            //Console.WriteLine(taskGeneric.Result);
            Console.ReadKey();

            
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
