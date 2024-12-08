using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Multithreading.Samples.Rx
{
    class RxScheduler : ISample
    {
        public void Run()
        {
            Console.WriteLine("Application threadId is {0}", Environment.CurrentManagedThreadId);
            var range = Observable.Range(0, 5);

            Console.WriteLine("NewThreadScheduler");
            range.ObserveOn(NewThreadScheduler.Default).Subscribe(OnNextAction);
            Task.Delay(100).Wait();
            Console.WriteLine("TaskPoolScheduler");
            range.ObserveOn(TaskPoolScheduler.Default).Subscribe(OnNextAction);
            Task.Delay(100).Wait();
            Console.WriteLine("ImmediateScheduler");
            range.ObserveOn(ImmediateScheduler.Instance).Subscribe(OnNextAction);
            Task.Delay(100).Wait();
            Console.WriteLine("CurrentThreadScheduler");
            range.ObserveOn(CurrentThreadScheduler.Instance).Subscribe(OnNextAction);

            Console.ReadKey();
        }

        private void OnNextAction(int item)
        {
            Console.WriteLine("Item {0} is processed on ThreadId {1}", item, Environment.CurrentManagedThreadId);
        }
    }
}
