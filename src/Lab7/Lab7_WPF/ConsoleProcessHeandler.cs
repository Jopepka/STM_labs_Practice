using System.Diagnostics;
using System.Text;

namespace Lab7_WPF
{
    internal class ConsoleProcessHeandler
    {
        private Process? _consoleProcess;
        public int CountThreads { get; private set; }

        public bool IsRun
        {
            get
            {
                try
                {
                    return !_consoleProcess.HasExited;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void StartProcess(string consolePath, string textPath, int tactReadDelay, int countReadCharsInTact, DataReceivedEventHandler handler)
        {
            if (IsRun)
                new Exception("Process is running. Stop the process first");

            var processInfo = new ProcessStartInfo()
            {
                FileName = consolePath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,

                Arguments = $"{textPath} {tactReadDelay} {countReadCharsInTact}",
            };

            _consoleProcess = new Process();
            _consoleProcess.StartInfo = processInfo;
            _consoleProcess.OutputDataReceived += handler;
            _consoleProcess.Start();
            _consoleProcess.BeginErrorReadLine();
            _consoleProcess.BeginOutputReadLine();
        }

        public void StartThread() => SendCommandToConsole($"START_NEW_THREAD");

        public void StopLastThread() => SendCommandToConsole($"STOP_LAST_THREAD");

        public void StopProcess() => _consoleProcess?.Kill();

        private void SendCommandToConsole(string command)
        {
            if (IsRun)
                _consoleProcess.StandardInput.WriteLine(command);
        }
    }
}
