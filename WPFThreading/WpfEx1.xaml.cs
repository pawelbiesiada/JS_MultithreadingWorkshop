using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFThreading
{
    //Rewrite program to run LongRunningTask asynchronously as a Task (use async/await)
    public partial class WpfEx1 : Window
    {
        private int Counter = 0;
        public WpfEx1()
        {
            InitializeComponent();
        }

        private void btnCounter_Click(object sender, RoutedEventArgs e)
        {
            Counter++;
            btnCounter.Content = Counter;
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var sc = SynchronizationContext.Current;
            tblText.Text = "ActionButtonPre";
            var action = new Action(LongRunningTask);
            var asyncResult = action.BeginInvoke(null, null);
            tblText.Text += Environment.NewLine + "ActionButtonPost";
            //action.EndInvoke(asyncResult); //why should not use endInvoke here?
        }

        private void LongRunningTask()
        {
            //tblText.Text += Environment.NewLine + "LongRunningTask";
            var sc = SynchronizationContext.Current;
            //BeginInvoke vs Invoke.
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
            {
                var sc1 = SynchronizationContext.Current;
                tblText.Text += Environment.NewLine + "LongRunningTask in Dispatcher";

                //Task.Delay(3000).Wait(); //you should avoid running long running processes in UI task where ever possible
            }));
            Task.Delay(2000).Wait();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tblText.Text = "";
        }
    }
}
