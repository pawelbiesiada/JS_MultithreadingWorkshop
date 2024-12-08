using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Multithreading.Async
{
    internal class ForeachAsync
    {
        public void Run()
        {
            Task.Run(RunOperationOnListAsync).Wait(); 
        }

        private async Task RunOperationOnListAsync()
        {
            var elements = GetElementsAsync();

            await foreach (var element in elements)
            {
                Console.WriteLine($"Awaited element: {element}");
            }

            //// await foreach behind the scenes
            //IAsyncEnumerator<int> e = elements.GetAsyncEnumerator();
            //try
            //{
            //    while (await e.MoveNextAsync())
            //    {
            //       Console.WriteLine($"Awaited element: {element}");
            //    }
            //}
            //finally { if (e != null) await e.DisposeAsync(); }
        }

        private async IAsyncEnumerable<int> GetElementsAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
