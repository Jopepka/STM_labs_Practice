using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Lab7_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process consoleProcess;
        private int threadCount = 0;
        private List<string> allOutput = new List<string>();

        private DispatcherTimer uiUpdateTimer;

        public MainWindow()
        {
            InitializeComponent();
            ThreadComboBox.Items.Add("Thread Main");
            ThreadComboBox.Items.Add("All threads");
            ThreadComboBox.SelectedIndex = 0;
            ThreadComboBox.SelectionChanged += ThreadComboBox_SelectionChanged;

            ItemsPerIterationTextBox.Text = "2";
            DelayTextBox.Text = "1000";
            ThreadCountTextBox.Text = "2";

            uiUpdateTimer = new DispatcherTimer();
            uiUpdateTimer.Interval = TimeSpan.FromMilliseconds(500);
            uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            uiUpdateTimer.Start();
        }

        private void UiUpdateTimer_Tick(object sender, EventArgs e)
        {
            while (outputQueue.TryDequeue(out string data))
            {
                allOutput.Add(data);
            }

            FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");
        }

        private void ThreadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");

        private void FilterAndDisplayOutput(string filterBy, bool showAll = false)
        {
            var filterText = string.Join("", showAll ? allOutput : DeletePrefixInCommandResultRow(Filter(filterBy)));
            DisplayOutput(filterText);
        }

        private IEnumerable<string> Filter(string? containString) =>
            allOutput.Where(line => containString is null || line.Contains(containString));

        private IEnumerable<string> DeletePrefixInCommandResultRow(IEnumerable<string> rows) => rows.Select(row => row.Split(": ")[1]);

        private void DisplayOutput(string text) => OutputTextBox.Text = text;

        private string? GetSelectedNameThread() => ThreadComboBox.Text;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsExistConsoleProcess())
                StartConsoleApp(GetConsolePath(), GetTactReadDelay(), GetCountReadCharsInTact());

            for (int i = 0; i < GetCountStartThreads(); i++)
                StartThread();
        }

        private bool IsExistConsoleProcess() => consoleProcess == null || consoleProcess.HasExited;

        private string GetConsolePath() => "E:\\Programming\\STM_labs_Practice\\src\\Lab7\\Lab7_WPF\\WarAndPeace.txt";

        private int GetTactReadDelay() => int.Parse(DelayTextBox.Text);

        private int GetCountReadCharsInTact() => int.Parse(ItemsPerIterationTextBox.Text);

        private int GetCountStartThreads() => int.Parse(ThreadCountTextBox.Text);

        private void StartConsoleApp(string consolePath, int tactReadDelay, int countReadCharsInTact)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "E:\\Programming\\STM_labs_Practice\\src\\Lab7\\Lab7_Console\\bin\\Debug\\net8.0\\Lab7_Console.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = $"{consolePath} {tactReadDelay} {countReadCharsInTact}",
            };

            consoleProcess = new Process();
            consoleProcess.StartInfo = processInfo;
            consoleProcess.OutputDataReceived += ConsoleProcess_OutputDataReceived;
            consoleProcess.Start();
            consoleProcess.BeginErrorReadLine();
            consoleProcess.BeginOutputReadLine();
        }

        private void StartThread()
        {
            threadCount++;
            ThreadComboBox.Items.Add($"Thread {threadCount}");
            SendCommandToConsole($"START_THREAD");
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (threadCount > 0)
            {
                SendCommandToConsole("STOP_THREAD");
                threadCount--;
                ThreadComboBox.Items.RemoveAt(ThreadComboBox.Items.Count - 1);
            }
            else if (consoleProcess != null && !consoleProcess.HasExited)
            {
                consoleProcess.Kill();
                consoleProcess = null;
            }
        }

        private void SendCommandToConsole(string command)
        {
            if (consoleProcess != null && !consoleProcess.HasExited)
                consoleProcess.StandardInput.WriteLine(command);
        }


        private ConcurrentQueue<string> outputQueue = new ConcurrentQueue<string>();

        private void ConsoleProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Dispatcher.BeginInvoke(() =>
                {
                    allOutput.Add(e.Data);
                    FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");
                });
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            consoleProcess.Kill();
        }
    }
}