using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AsynchronousTests
{
    [TestFixture]
    public class Tests
    {

        //it passes, but anything could happen inside and it would still pass anyway
        [Test]
        public void TestVoidAsyncMethod()
        {
            var obj = new ProductionReadyClass();

            obj.DoVoidStuffAsync();

            Assert.Pass();
        }

        //It actually passes but it should really not
        [Test]
        public void TestVoidAsyncExceptionMethod_ThatShouldFailButItDoesNot()
        {
            var obj = new ProductionReadyClass();

            obj.DoVoidStuffAndFailAsync();

            Assert.Pass();
        }

        //Cannot await on void
        //[Test]
        //public async Task TestVoidAsyncMethodAsync()
        //{
        //    var obj = new ProductionReadyClass();

        //    await obj.DoVoidStuffAsync();

        //    Assert.Pass();
        //}

        //Cannot await on void in delegate
        //[Test]
        //public void TestVoidAsyncExceptionMethod_ThatShouldFailButItDoesNot()
        //{
        //    var obj = new ProductionReadyClass();

        //   // Assert.ThrowsAsync<ApplicationException>(() => obj.DoVoidStuffAndFailAsync() );  //Does not match delegate signature
        //}





        //This actually passes but should really not
        [Test]
        public void TestAsyncExceptionMethod()
        {
            var obj = new ProductionReadyClass();

            obj.DoStuffAndFailAsync();

            Assert.Pass();
        }

        //This works, but you should not mix async/await with Wait()
        //also and you might deadlock your code in context environment
        [Test]
        public void TestAsyncMethod()
        {
            var obj = new ProductionReadyClass();

            var t = obj.DoStuffAsync();
            t.Wait();  

            Assert.IsTrue(t.Result);
        }

        //async test method works cause nunit supports it
        [Test]
        public async Task TestAsyncMethodAsync()
        {
            var obj = new ProductionReadyClass();

            var result = await obj.DoStuffAsync();

            Assert.IsTrue(result);
        }

        //this is recommended implementation
        [Test]
        public void TestAsyncMethodAsyncWithAssert()
        {
            var obj = new ProductionReadyClass();

            Assert.DoesNotThrowAsync(async () => await obj.DoStuffAsync());
            
            //Assert.ThrowsAsync<ApplicationException>(async () => await obj.DoStuffAsync());

            Assert.Pass();
        }

        //This does not pass but it should
        [Test]
        public async Task TestAsyncExceptionMethodAsync()
        {
            var obj = new ProductionReadyClass();

            Assert.Throws<ApplicationException>(() => obj.DoStuffAndFailAsync());

            Assert.Pass();
        }

        //this is a correct way of testin async methods
        [Test]
        public void TestAsyncExceptionMethodAsyncWithAsyncInAssert()
        {
            var obj = new ProductionReadyClass();

            Assert.ThrowsAsync<ApplicationException>(async () => await obj.DoStuffAndFailAsync());
        }

        //this also works, however is not a recommended implementation, delegate will execute synchronously
        [Test]
        public void TestAsyncExceptionMethodAsyncWithAssert()
        {
            var obj = new ProductionReadyClass();

            Assert.ThrowsAsync<ApplicationException>(() => obj.DoStuffAndFailAsync());
        }

        //this causes runtime exception can't use async/await in delegate
        //[Test]
        //public void TestAsyncExceptionMethodAsyncWithNonAsyncAssert()
        //{
        //    var obj = new ProductionReadyClass();

        //    Assert.Throws<ApplicationException>(async () => await obj.DoStuffAndFailAsync());
        //}
    }
}