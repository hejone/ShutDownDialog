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

namespace ShutdownDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth / 2;
            Top = SystemParameters.PrimaryScreenHeight / 3;
            Topmost = true;
        }

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            Activate();
        }


        private void shutDownClick(object sender, RoutedEventArgs e)
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

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
