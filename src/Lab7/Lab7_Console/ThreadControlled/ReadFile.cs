using System.Text;

namespace Lab7_Console.Task
{
    internal class ReadFile : IThreadControlled
    {
        private readonly string _filePath;
        private readonly int _itemsPerTact;
        private readonly int _millisecondTactDelay;
        private readonly Action<string> Write;
        public bool IsStop { get; private set; }

        public ReadFile(string filePath, int itemsPerTact, int millisecondTactDelay, Action<string> Write)
        {
            _filePath = filePath;
            _itemsPerTact = itemsPerTact;
            _millisecondTactDelay = millisecondTactDelay;
            this.Write = Write;
        }

        public void Start()
        {
            using StreamReader sr = new StreamReader(_filePath, Encoding.UTF8);
            while (!sr.EndOfStream && !IsStop)
            {
                for (int i = 0; i < _itemsPerTact && !sr.EndOfStream; i++)
                    Write($"Thread {Thread.CurrentThread.Name}: {sr.ReadLine()}"); ;

                Thread.Sleep(_millisecondTactDelay);
            }
        }

        public void Stop() => IsStop = true;
    }
}
