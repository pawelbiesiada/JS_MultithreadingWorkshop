using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Multithreading.Samples.Rx
{
    internal class RxBuffer : ISample
    {
        public void Run()
        {


            int bufferCount = 0;
            var range = Observable.Range(0, 10)               
            .Buffer(3);
            range.Subscribe(
                (list) => { OnNextAction(list, bufferCount++); },   //OnNext
                (ex) => { Console.WriteLine(ex.Message); },                 //OnError
                () => { Console.WriteLine("Processing completed."); }        //OnComplete
            );

            Console.ReadKey();
        }

        private void OnNextAction(IList<int> list, int bufferNum)
        {
            Task.Delay(300).Wait();
            Console.WriteLine("Buffer block #{0}", bufferNum+1);
            foreach (var item in list)
            {
                Console.Write(new string('\t', bufferNum));
                Console.WriteLine("element: {0}", item);
            }
        }
    }
}
