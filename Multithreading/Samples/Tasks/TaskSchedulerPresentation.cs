using System;
using System.Threading.Tasks;

namespace Multithreading.Samples.Tasks
{
    internal class TaskSchedulerPresentation : ISample
    {
        public void Run()
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("I was enqueued on the thread pool's global queue. SchedulerID {0}", Task.Factory.Scheduler?.Id);

                // TaskB is a nested task and TaskC is a child task. Both go to local queue.
                Task taskB = new Task(() => Console.WriteLine("I was enqueued on the local queue. SchedulerID {0}", Task.Factory.Scheduler.Id));
                Task taskC = new Task(() => Console.WriteLine("I was enqueued on the local queue, too. SchedulerID {0}", Task.Factory.Scheduler.Id),
                                        TaskCreationOptions.AttachedToParent);

                taskB.Start();
                taskC.Start();
            });
            Console.ReadKey();
        }
    }
}
