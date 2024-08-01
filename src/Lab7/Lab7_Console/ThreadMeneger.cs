using Lab7_Console.Task;

namespace Lab7_Console
{
    internal class ThreadManager
    {
        private Func<IThreadControlled> GetNewTask;
        private List<ThreadTask> _threads = new List<ThreadTask>();
        private event Action<string>? Notify;

        public ThreadManager(Func<IThreadControlled> GetNewTask) => this.GetNewTask = GetNewTask;

        public void Subscribe(Action<string> Notify) => this.Notify += Notify;

        public void Run(Func<Commands> GetCommand)
        {
            LogCommand("Thread Main: The application is running");

            while (true)
            {
                LogCommand("Thread Main: Wait a command");

                var command = GetCommand();

                LogCommand($"Thread Main: Get command = {command}");

                switch (command)
                {
                    case Commands.StartNewThread:
                        StartNewThread();
                        break;
                    case Commands.StopLastThread:
                        StopLastThread();
                        break;
                    default:
                        LogCommand("Thread Main: Wrong command");
                        break;
                }
            }
        }

        private void StartNewThread()
        {
            var thread = new ThreadTask(GetNewTask());
            _threads.Add(thread);
            thread.ThreadStart();

            LogCommand("Thread Main: New thread started");
        }

        private void StopLastThread()
        {
            if (_threads.Count > 0)
            {
                _threads[^1].ThreadStop();
                _threads.RemoveAt(_threads.Count - 1);
                LogCommand("Thread Main: Last thread stopped");
            }
            else
                LogCommand("No threads to stop");
        }

        private void LogCommand(string message) => Notify?.Invoke(message);
    }
}
