using System;
using System.Windows;

namespace MouseController
{
    public partial class MainWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool SetCursorPos(int X, int Y);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_MOVE = 0x01;

        private int round, interval, repeat;
        private string commands;
        
        public MainWindow()
        {
            InitializeComponent();
            SetupBackgroundTasks();
            round = 0;
            commands = "";
            interval = 500;
            repeat = 1;
        }

        private void SetupBackgroundTasks()
        {
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(DuringWork);
            worker.RunWorkerAsync();
        }

        void DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;
            while(true)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                System.Threading.Thread.Sleep(100);
                worker.ReportProgress(0, 0);
            }
        }

        void DuringWork(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            CursorStatus.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            RepeatStatus.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            CursorStatus.Content = String.Format("Cursor Position = ({0:D},{1:D})", System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            RepeatStatus.Content = "Round -";
            if (round > 0)
            {
                CursorStatus.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                RepeatStatus.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                RepeatStatus.Content = String.Format("Round {0:D}", round);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            commands = Commands.Text.ToUpper();
            interval = Int32.Parse(Interval.Text);
            repeat = Int32.Parse(Repeat.Text);
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(ControlMouse);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(EndControlMouse);
            worker.RunWorkerAsync();
        }

        void ControlMouse(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;
            int x = 0, y = 0;
            SetCursorPos(x, y);
            for (round = 1; round <= repeat; round++)
            {
                foreach (string command in System.Text.RegularExpressions.Regex.Split(commands, @"\s+"))
                {
                    if (System.Windows.Forms.Cursor.Position.X != x || System.Windows.Forms.Cursor.Position.Y != y)
                    {
                        round = repeat;
                        break;
                    }
                    if (command.StartsWith("CLICK"))
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }
                    else if (command.StartsWith("DOWN"))
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    }
                    else if (command.StartsWith("UP"))
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }
                    else if (command.StartsWith("SET"))
                    {
                        x = Int32.Parse(System.Text.RegularExpressions.Regex.Split(command, @"_")[1]);
                        y = Int32.Parse(System.Text.RegularExpressions.Regex.Split(command, @"_")[2]);
                        SetCursorPos(x, y);
                    }
                    else if (command.StartsWith("MOD"))
                    {
                        x += Int32.Parse(System.Text.RegularExpressions.Regex.Split(command, @"_")[1]);
                        y += +Int32.Parse(System.Text.RegularExpressions.Regex.Split(command, @"_")[2]);
                        SetCursorPos(x, y);
                    }
                    System.Threading.Thread.Sleep(interval);
                }
            }
            round = 0;
            worker.ReportProgress(0, 0);
        }

        void EndControlMouse(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Start.IsEnabled = true;
        }
    }
}
