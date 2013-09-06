using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace WpfThreadTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread countThread;

        public MainWindow()
        {
            InitializeComponent();
            //this.textBox1.Text = DateTime.Now.ToLocalTime().ToString("yyyy年MM月dd日 hh:mm:ss tt");
            this.textBox1.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss tt");
            //textBox1.Text = Convert.ToString(DateTime.Now.ToUniversalTime());
            countThread = new Thread(new ThreadStart(DispatcherThread));
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (button3.Content.ToString() == "开始时间线程")
            {
                button3.Content = "停止时间线程";
                if (countThread.ThreadState == ThreadState.Suspended)
                {
                    //线程继续
                    countThread.Resume();
                }
                else
                    countThread.Start();
            }
            else
            {
                button3.Content = "开始时间线程";
                //线程挂起
                countThread.Suspend();
            }
        }
        public void DispatcherThread()
        {
            //可以通过循环条件来控制UI的更新
            while (true)
            {
                ///线程优先级，最长超时时间，方法委托（无参方法）
                textBox1.Dispatcher.Invoke(
                DispatcherPriority.Normal, TimeSpan.FromSeconds(1), new Action(UpdateTime));
                Thread.Sleep(1000);
            }
        }


        private void UpdateTime()
        {
            //this.textBox1.Text = DateTime.Now.ToLocalTime().ToString("yyyy年MM月dd日 hh:mm:ss tt");
            this.textBox1.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss tt");
            //textBox1.Text = Convert.ToString(DateTime.Now.ToUniversalTime());
            //this.textBox1.Text = Convert.ToString(TimeSpan.FromDays(1.33));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            countThread.Abort();
            Application.Current.Shutdown();
        }
    }
}