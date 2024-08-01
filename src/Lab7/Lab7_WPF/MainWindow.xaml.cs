using Microsoft.Win32;
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

        private ConsoleProcessHeandler consoleProcess = new ConsoleProcessHeandler();
        private DispatcherTimer uiUpdateTimer = new DispatcherTimer();
        private bool isShowWarning = false;

        public MainWindow()
        {
            InitializeComponent();

            ResetThreadsComboBox();
            ThreadComboBox.SelectionChanged += ThreadComboBox_SelectionChanged;

            ItemsPerIterationTextBox.Text = "1";
            DelayTextBox.Text = "1000";
            ThreadCountTextBox.Text = "1";
            ConsolePathTextBox.Text = "";
            TextPathTextBox.Text = "MyDiploma.txt";

            uiUpdateTimer.Interval = TimeSpan.FromMilliseconds(500);
            uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            uiUpdateTimer.Start();
        }

        private string GetConsolePath() => ConsolePathTextBox.Text;
        private string GetTextPath() => TextPathTextBox.Text;
        private int GetTactReadDelay() => int.Parse(DelayTextBox.Text);
        private int GetCountReadCharsInTact() => int.Parse(ItemsPerIterationTextBox.Text);
        private int GetCountStartThreads() => int.Parse(ThreadCountTextBox.Text);
        private string GetSelectedNameThread() => ThreadComboBox.Text;
        private void UpdateDisplayOutput(string text) => OutputTextBox.Text = $"{text}";

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            if (isShowWarning && !consoleProcess.IsRun && AskReloadConsoleApp())
            {
                allOutput = new List<string>();
                outputQueue = new ConcurrentQueue<string>();

                ResetThreadsComboBox();
                appSettingsGroupBox.IsEnabled = true;

                isShowWarning = false;
                return;
            }

            if (!consoleProcess.IsRun)
            {
                try
                {
                    consoleProcess.StartProcess(
                        GetConsolePath(),
                        GetTextPath(),
                        GetTactReadDelay(),
                        GetCountReadCharsInTact(),
                        ConsoleProcess_OutputDataReceived);
                    appSettingsGroupBox.IsEnabled = false;
                    isShowWarning = true;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            for (int i = 0; i < GetCountStartThreads(); i++, threadCount++)
            {
                consoleProcess.StartThread();
                AddThreadInThreadComboBox();
            }
        }

        private void ResetThreadsComboBox()
        {
            ThreadComboBox.Items.Clear();
            ThreadComboBox.Items.Add("All threads");
            ThreadComboBox.Items.Add("Thread Main");
            ThreadComboBox.SelectedIndex = 0;
        }

        private bool AskReloadConsoleApp() =>
            MessageBox.Show("Предыдущая программа завершила выполнение, новый запуск очистит предыдущие результаты", "New start", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;

        private void Enable_ItemsPerIterationTextBox_DelayTextBox()
        {
            ItemsPerIterationTextBox.IsEnabled = true;
            DelayTextBox.IsEnabled = true;
        }

        // ThreadComboBox.Items.Count - 1 т.к. в комбо-боксе уже лежат All threads и Main Thread, а счет ведется потоков с 1
        private void AddThreadInThreadComboBox() =>
            ThreadComboBox.Items.Add($"Thread {ThreadComboBox.Items.Count - 1}");

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            consoleProcess.StopLastThread();
        }

        private void ThreadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            FilterAndDisplayOutput(GetSelectedNameThread(), GetSelectedNameThread() == "All threads");

        private void FilterAndDisplayOutput(string filterBy, bool showAll = false)
        {
            var filterText = string.Join("\n", showAll ? allOutput : DeletePrefixInCommandResultRow(Filter(filterBy)));
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
            consoleProcess.StopProcess();
        }

        private void ChangeConsolePathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                ConsolePathTextBox.Text = openFileDialog.FileName;
        }

        private void ChangeTextPathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                TextPathTextBox.Text = openFileDialog.FileName;
        }
    }
}