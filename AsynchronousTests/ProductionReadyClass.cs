using System;
using System.Threading.Tasks;

namespace AsynchronousTests
{
    public class ProductionReadyClass
    {
        public async Task<bool> DoStuffAsync()
        {
            await Task.Delay(100); // intensive async processing

            return true;
        }

        public async Task<bool> DoStuffAndFailAsync()
        {
            await Task.Delay(100); // intensive async processing

            throw new ApplicationException();
        }


        public async void DoVoidStuffAsync()
        {
            await Task.Delay(100); // intensive async processing

            Console.WriteLine("Stuff completed");
        }

        public async void DoVoidStuffAndFailAsync()
        {
            await Task.Delay(100); // intensive async processing

            throw new ApplicationException();
        }
    }
}
