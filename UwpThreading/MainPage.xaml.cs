using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpThreading
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //throw new Exception();
            var t = UpdateTextBlock();
            var sc = SynchronizationContext.Current;
            await t;
            var s1 = SynchronizationContext.Current;
        }

        private async Task UpdateTextBlock()
        {
            var sc = SynchronizationContext.Current;

            var t = Task.Factory.StartNew(() =>
            {
                var sc2 = SynchronizationContext.Current;

                var dispatcherHandler = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        var s2 = SynchronizationContext.Current;
                        for (int i = 0; i < 5; i++)
                        {
                            
                            tbText.Text += "SomeText\n";
                            await Task.Delay(1000);
                        }
                    });
                //tbText.Text += "SomeTextTask\n";// -- this causes the exception
            });
            //Task.Delay(2000).Wait(); // this will block UI
            await t;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbText.Text = "";
        }
    }
}
