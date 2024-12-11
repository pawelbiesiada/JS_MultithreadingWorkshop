using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Multithreading.Async;
using MyLib;

namespace MyWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.Loaded

            Observable.FromEventPattern(this, "Loaded")
                .Subscribe( x => { });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var asyncPresentation = new AsyncAwaitPresentation();

            asyncPresentation.Run();




            var myClass = new Class1();
            await myClass.MyMethAsync();
        }
    }
}