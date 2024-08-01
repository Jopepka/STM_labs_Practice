using Lab7_Console;
using System.Text;
class Program
{
    static List<Thread> threads = new List<Thread>();
    static bool running = true;

    static string filePath;
    static int millisecondTactDelay;
    static int itemsPerTact;
    static int countThread = 0;

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        filePath = args[0];
        millisecondTactDelay = int.Parse(args[1]);
        itemsPerTact = int.Parse(args[2]);

        var threadManager = new ThreadManager(ReadFileTask);
        threadManager.Subscribe(Console.WriteLine);
        threadManager.Run(GetCommandsFromConsole);
    }

    static IThreadControlled ReadFileTask() => new ReadFile(filePath, itemsPerTact, millisecondTactDelay, Console.WriteLine);

    static Commands GetCommandsFromConsole()
    {
        while (true)
        {
            switch (Console.ReadLine())
            {
                case "START_NEW_THREAD":
                    return Commands.StartNewThread;
                case "STOP_LAST_THREAD":
                    return Commands.StopLastThread;
                default:
                    continue;
            }
        }
    }
}
