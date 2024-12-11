using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Exercises.Workshop
{
    public class ParallelReadDirectoryFactory
    {
        public async Task<ParallelReadDirectory> Create()
        {
            var t = Task.Run(() => { return new ParallelReadDirectory(); });

            return await t;
        }
    }


    public class ParallelReadDirectory : IAsyncDisposable, IDisposable
    {
        public ParallelReadDirectory() 
        { 

        }



        public void Dispose()
        {
            
        }

        public async ValueTask DisposeAsync()
        {
            // await ....
            Dispose();
        }

        public async Task ReadDirectory()
        {

            await Parallel.ForEachAsync(Directory.GetFiles(@"C:\Temp"), CancellationToken.None,
            (path, ct) => 
            {
                return new ValueTask(new Task(() => { Console.WriteLine($"File name: {path}. Size: {new FileInfo(path).Length / 1024} KB "); }));
            });
        }
    }
}
