using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Samples.Tasks
{
    class TasksCancellationToken : ISample
    {
        public void Run()
        { 
            var cts = new CancellationTokenSource();
            
            //var task = Task.Run(new Action(LongRunningProcessDelay));
            var task = new Task((token) => LongRunningProcess((CancellationToken)token), cts.Token);


            try
            {
                task.Start();
                Task.Delay(3000).Wait();
                //cts.Cancel(true);
                cts.CancelAfter(3000); //can be used as timeout

                task.Wait();

                Console.WriteLine("Task completed");
                if (task.Exception != null)
                    throw task.Exception;
            }
            catch(OperationCanceledException oEx)
            {
                Console.WriteLine("Operation cancelled.");
                Console.WriteLine(oEx);
            }
            catch(AggregateException ex)
            {
                if(ex.InnerException is OperationCanceledException)
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main thread.");
                Console.WriteLine(ex);
            }


            Console.ReadKey();
        }

        private void LongRunningProcess(CancellationToken ct)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    ct.ThrowIfCancellationRequested(); //throws OperationCanceledException
                    Thread.Sleep(1000);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LongRunningProcessGracefullCancellation(CancellationToken ct)
        {
            for (int i = 0; i < 10; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    ///
                    ///
                    return;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
