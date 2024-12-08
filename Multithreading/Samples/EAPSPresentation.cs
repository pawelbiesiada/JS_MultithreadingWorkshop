using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Multithreading.Samples
{
    public class GetContentCompletedEventArgs : EventArgs
    {
        public GetContentCompletedEventArgs(string url, string result)
        {
            Url = url;
            Result = result;
        }

        public string Url { get; }
        public string Result { get; }
    }

    public delegate void GetContentCompletedEventHandler(object sender,
        GetContentCompletedEventArgs e);
    
    internal class EAPSPresentation
    {
        public bool IsBusy { get; private set; }

        public string GetContent(string url)
        {
            var task = GetContentTask(url, null);
            task.Wait();
            return task.Result;
        }

        public void GetContentAsync(string url)
        {
            IsBusy = true;

            var task = GetContentTask(url, (result) => 
            {
                IsBusy = false;

                if (GetContentCompleted != null)
                {
                    GetContentCompleted.Invoke(this,
                        new GetContentCompletedEventArgs(url, result));
                }
            });
        }

        public event GetContentCompletedEventHandler GetContentCompleted;

        //Optionally we may implement ProgressEchanged event
        //public event GetContentProgressChangeEventArgs GetContentProgressChanged;
        public void CancelAsync()
        {
            throw new NotImplementedException();
        }
        

        private Task<string> GetContentTask(string url, Action<string> postAction)
        {

            var task = Task.Factory.StartNew(() =>
            {
                var content = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create(url);
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }

                if (postAction != null)
                {
                    postAction(content);
                }

                return content;
            });

            return task;
        }
    }
}
