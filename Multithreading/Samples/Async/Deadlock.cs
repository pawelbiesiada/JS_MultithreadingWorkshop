using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Async
{
    internal class Deadlock
    {
        public void Run()
        {
            var sc1 = SynchronizationContext.Current;
            RunOperationWithDeadlockAsync().Wait(); // no deadlock
        }

        private async Task RunOperationWithDeadlockAsync()
        {
            await Task.Delay(300);
        }
    }
}
