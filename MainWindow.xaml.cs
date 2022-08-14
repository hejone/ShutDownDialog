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
        private int timerStartValue = 10;
        private int timer;
        private string defaultMode = "s";
        private string countDownText = "Automatically {0} after: ";
        public MainWindow()
        {   
            InitializeComponent();
            Button defaultButton = shutDownButton;
            string windowText = "shutdown";
            CommandlineArgs cla = new CommandlineArgs();
            if(cla.ReadArgs.ContainsKey("time")) {
                timerStartValue = int.Parse(cla.ReadArgs["time"]);
            } 
            else if(cla.ReadArgs.ContainsKey("default")) {
                defaultMode = cla.ReadArgs["default"];
                if (defaultMode == "restart") {
                    defaultButton = restartButton;
                    windowText = "restart";
                }
            }
            defaultButton.IsDefault = true;
            countDownText = string.Format(countDownText, windowText);
            timer = timerStartValue;
            Left = SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            Top = SystemParameters.PrimaryScreenHeight / 3;
            Topmost = true;
            
            string _countText = $"{countDownText} {String.Format("{0:00}", timer)} s";
            countDownLabel.Text = _countText;
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
        }
        
        /// <summary>Executes shutdown command with parameter</summary>
        /// <param name="mode">Mode for shut down. Valid values are "s", "r" and "l", 
        /// where "s" = Shutdown, "r" = Restart and "l" = Logout. Default is "s"</param>
        private void Shutdown(string mode="s")
        {
            List<string> validModes = new List<string>() {
                "s", "r", "l"
            };

            if(validModes.IndexOf(mode) == -1)
            {
                throw new ArgumentException($"Invalid mode {mode}");
            }

            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $@"/C C:\Windows\System32\shutdown.exe /{mode} /t 0"
            };
            Process process = new Process() {
                StartInfo = startInfo
            };
            process.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {   
           timer -= 1;
            countDownLabel.Text = $"{countDownText} {String.Format("{0:00}", timer)} s";
            if(timer == 0) {
                Shutdown();
            } 
            else if(timer < 0) {
                // If the timer somehow goes under 0, reset it!
                // This can happen if the uset puts the computer to sleep during the countdown,
                // The Shutdown command cannot be executed during sleep procedures
                timer = timerStartValue;
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

        private void restartClick(object sender, RoutedEventArgs e)
        {   
            Shutdown("r");
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
