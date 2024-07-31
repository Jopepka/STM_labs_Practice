
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows.Documents;

namespace Lab7_WPF
{
    internal class ConsoleProccessHeandler
    {
        private Process? _consoleProcess;
        public bool IsRun { get; private set; }

        public void StartConsoleApp(string consolePath, int tactReadDelay, int countReadCharsInTact, DataReceivedEventHandler handler)
        {
            if (IsRun)
                return;

            var processInfo = new ProcessStartInfo()
            {
                FileName = "E:\\Programming\\STM_labs_Practice\\src\\Lab7\\Lab7_Console\\bin\\Debug\\net8.0\\Lab7_Console.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = $"{consolePath} {tactReadDelay} {countReadCharsInTact}",
            };

            _consoleProcess = new Process();
            _consoleProcess.StartInfo = processInfo;
            _consoleProcess.OutputDataReceived += handler;
            _consoleProcess.Start();
            _consoleProcess.BeginErrorReadLine();
            _consoleProcess.BeginOutputReadLine();

            IsRun = true;
        }

        public void StartThread() => SendCommandToConsole($"START_THREAD");
        public void StopLastThread() => SendCommandToConsole($"STOP_THREAD");

        public void StopConsoleApp() => _consoleProcess.Kill();

        private void SendCommandToConsole(string command)
        {
            if (_consoleProcess != null && !_consoleProcess.HasExited)
                _consoleProcess.StandardInput.WriteLine(command);
        }
    }
}
