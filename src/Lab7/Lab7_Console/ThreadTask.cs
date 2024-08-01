using Lab7_Console.Task;

namespace Lab7_Console
{
    internal class ThreadTask
    {
        IThreadControlled _doTask;
        Thread? _thread;

        public ThreadTask(IThreadControlled doTask) => _doTask = doTask;

        public void ThreadStart()
        {
            if (_thread == null)
            {
                _thread = new Thread(_doTask.Start);
                _thread.Start();
            }
        }

        public void ThreadStop() => _doTask.Stop();
    }
}
