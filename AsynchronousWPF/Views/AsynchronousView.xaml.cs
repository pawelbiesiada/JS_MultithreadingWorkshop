using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AsynchronousWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AsynchronousView : Window
    {
        private CancellationTokenSource cts;

        private bool isFeatureOn = true;

        private IDisposable txtContentTextChangedListener;

        public AsynchronousView()
        {
            InitializeComponent();
            try
            {
               txtContentTextChangedListener = textChangedObservable();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private IDisposable textChangedObservable()
        {
            return Observable
                .FromEventPattern<RoutedEventArgs>(txtContent, "TextChanged")
                .Where(e => isFeatureOn)
                .Where(e => txtContent.Text.Length >= 3)                
                .Throttle(TimeSpan.FromMilliseconds(500))
                .ObserveOnDispatcher()
                .Subscribe((e) =>
                {
                    var tb = (TextBox)e.Sender;

                    tb.Text = "Hi";
                    /// wyszukiwanie
                });
        }

        public void StopSearchOnTyping()
        {
            txtContentTextChangedListener.Dispose();
        }
        public void StartSearchOnTyping()
        {
            txtContentTextChangedListener = textChangedObservable();
        }

        private async Task Tester()
        {
            try
            {
                var t =  btnReadFile_ClickAsync(null, null);

                //t.Wait(); //AggregateException  InnerException -> FileNotFoundException
                await t; //FileNotFoundException
            }
            catch (AggregateException)
            {
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception)
            {
            }
        }

        private async void btnReadFile_Click(object sender, RoutedEventArgs e)
        { 
            cts = new CancellationTokenSource();
            try
            {
                using (var reader = new StreamReader("C:\\Temp\\test.txt"))
                {
                    txtContent.Text = await Task.Factory.StartNew( () => { return reader.ReadToEnd(); } , cts.Token );
                }

            }
            catch (Exception)
            {

            }

            //txtContent.Text = await File.ReadAllTextAsync(txtContent.Text);
        }



        private async Task btnReadFile_ClickAsync(object sender, RoutedEventArgs e)
        {
            using (var reader = new StreamReader("C:\\Temp\test.txt"))
            {
                txtContent.Text = await reader.ReadToEndAsync();
            }

            //txtContent.Text = await File.ReadAllTextAsync(txtContent.Text);
        }



        private async Task<bool> IsFileRead(string path)
        {
            using (var reader = new StreamReader("C:\\Temp\test.txt"))
            {
                txtContent.Text = await reader.ReadToEndAsync();
                return true;
            }

            return false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = Task.Run(() => 
            {
                return "Hi there";
            }
            ).ConfigureAwait(false);

            txtContent.Text = "Hi";

           // txtContent.Text = await t;

            txtContent.Text = "Hi";





            var t2 = Task.Run(() =>
            {
                /////
                //
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
                {
                    txtContent.Text = "Hi there";

                }));

                var test = "";
                ///

                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
                {
                    txtContent.Text += "Hi there";
                    //
                    test = "sth";
                }));

                Console.WriteLine(test);
                ///
                ///
            }
            );



            cts?.Cancel();
        }

        private CancellationTokenSource _cts;

        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtContent.Text.Length < 3)
                return;

            if(_cts != null)
            {
                _cts.Cancel();
            }

            _cts = new CancellationTokenSource();

            var timeout = Task.Delay(500, _cts.Token);



            timeout.ContinueWith(t => { }, _cts.Token); 

            _cts.Cancel();

        }
    }
}
