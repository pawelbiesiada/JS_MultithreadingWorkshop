using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Threading;

namespace WinFormsThreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "ActionButtonPre"; //this is ok because it is UI thread
                                               //throw new Exception();

            var sc = SynchronizationContext.Current;
            var task = Task.Factory.StartNew(LongRunningTask);//.ConfigureAwait(false);

            textBox1.Text += Environment.NewLine + "ActionButtonPreAwait";
            await task;

            var sc1 = SynchronizationContext.Current;
            textBox1.Text += Environment.NewLine + "ActionButtonPost";
        }


        private void LongRunningTask()
        {
            var sc = SynchronizationContext.Current;
            Task.Delay(3000).Wait();
            textBox1.BeginInvoke(new Action(() => {
                var sc2 = SynchronizationContext.Current;
                textBox1.Text += Environment.NewLine + "LongRunningTask"; //this is ok because it is UI thread
                button1.Text = "Clicked";
            }));
            //textBox1.Text = "This will cause exception.";  //this is not ok because this is not UI thread
        }

    }
}
