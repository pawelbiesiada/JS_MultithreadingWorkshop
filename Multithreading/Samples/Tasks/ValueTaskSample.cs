using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading.Tasks
{
    class ValueTaskSample
    {
        public async void Run()
        {
            var test = new Repository<object>();

            var vt = test.GetData();
            var result = await vt;

            var vt2 = test.GetDataAsync();
            var result2 = await vt2;
        }
    }

    public class Repository<T>
    {
        public ValueTask<T> GetData()
        {
            var value = default(T);
            return new ValueTask<T>(value);
        }

        public async ValueTask<T> GetDataAsync()
        {
            var value = default(T);
            await Task.Delay(1000);
            return value;
        }
    }

}
