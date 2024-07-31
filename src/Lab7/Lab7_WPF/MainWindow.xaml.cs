using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Lab7_WPF
{
    public partial class MainWindow : Window
    {
        private int threadCount = 0;
        private List<string> allOutput = new List<string>();
        private ConcurrentQueue<string> outputQueue = new ConcurrentQueue<string>();

        private ConsoleProccessHeandler consoleProcess = new ConsoleProccessHeandler();
        private DispatcherTimer uiUpdateTimer = new DispatcherTimer();

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

            uiUpdateTimer.Interval = TimeSpan.FromMilliseconds(500);
            uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            uiUpdateTimer.Start();
        }

        private string GetConsolePath() => "E:\\Programming\\STM_labs_Practice\\src\\Lab7\\Lab7_WPF\\WarAndPeace.txt";
        private int GetTactReadDelay() => int.Parse(DelayTextBox.Text);
        private int GetCountReadCharsInTact() => int.Parse(ItemsPerIterationTextBox.Text);
        private int GetCountStartThreads() => int.Parse(ThreadCountTextBox.Text);
        private string GetSelectedNameThread() => ThreadComboBox.Text;
        private void UpdateDisplayOutput(string text) => OutputTextBox.Text = text;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!consoleProcess.IsRun)
                consoleProcess.StartConsoleApp(GetConsolePath(), GetTactReadDelay(), GetCountReadCharsInTact(), ConsoleProcess_OutputDataReceived);

            for (int i = 0; i < GetCountStartThreads(); i++)
            {
                consoleProcess.StartThread();
                AddThreadInThreadComboBox();
            }
        }

        // ThreadComboBox.Items.Count - 1 т.к. в комбо-боксе уже лежат All threads и Main Thread, а счет ведется с 1
        private void AddThreadInThreadComboBox() =>
            ThreadComboBox.Items.Add($"Thread {ThreadComboBox.Items.Count - 1}");

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            consoleProcess.StopLastThread();
            ThreadComboBox.Items.RemoveAt(ThreadComboBox.Items.Count - 1);
        }

        private void ThreadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");

        private void FilterAndDisplayOutput(string filterBy, bool showAll = false)
        {
            var filterText = string.Join("", showAll ? allOutput : DeletePrefixInCommandResultRow(Filter(filterBy)));
            UpdateDisplayOutput(filterText);
        }

        private IEnumerable<string> Filter(string? containString) =>
            allOutput.Where(line => containString is null || line.Contains(containString));

        private IEnumerable<string> DeletePrefixInCommandResultRow(IEnumerable<string> rows) => rows.Select(row => row.Split(": ")[1]);

        private void ConsoleProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                outputQueue.Enqueue(e.Data);
        }
        private void UiUpdateTimer_Tick(object sender, EventArgs e)
        {
            while (outputQueue.TryDequeue(out string data))
                allOutput.Add(data);

            FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            consoleProcess.StopConsoleApp();
        }
    }
}