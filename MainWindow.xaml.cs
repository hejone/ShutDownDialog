using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Diagnostics;
using System.Windows.Threading;

namespace ShutdownDialog
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        private int timer = 30;
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth / 2;
            Top = SystemParameters.PrimaryScreenHeight / 3;
            Topmost = true;

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
        }

        private void Shutdown()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C C:\Windows\System32\shutdown.exe /s /t 0"
            };
            Process process = new Process() {
                StartInfo = startInfo
            };
            process.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {   
           timer -= 1;
            countDownLabel.Text = $"{timer} s";
            if(timer == 0) {
                Shutdown();
            }
        }

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            Activate();
        }

        private void shutDownClick(object sender, RoutedEventArgs e)
        {   
            Shutdown();
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
