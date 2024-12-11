namespace MyLib
{
    public class Class1
    {
        public async Task MyMethAsync()
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
