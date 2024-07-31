class Program
{
    static List<Thread> threads = new List<Thread>();
    static bool running = true;

    static string filePath;
    static int millisecondDelay;
    static int itemsPerIteration;
    static int countThread = 0;

    static void Main(string[] args)
    {
        Console.WriteLine($"Thread Main: Count args = {args.Length}");
        Console.WriteLine("Thread Main: args:");
        foreach (var item in args)
            Console.WriteLine($"Thread Main: {item}");

        filePath = args[0];
        millisecondDelay = int.Parse(args[1]);
        itemsPerIteration = int.Parse(args[2]);

        Run();
    }

    private static void Run()
    {
        Console.WriteLine("Thread Main: The application is running");
        while (running)
        {
            Console.WriteLine("Thread Main: Wait a command");

            switch (Console.ReadLine())
            {
                case "START_THREAD":
                    StartThread();
                    break;
                case "STOP_THREAD":
                    StopThread();
                    break;
                case "EXIT":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Thread Main: Wrong command");
                    break;
            }
        }
    }

    private static void StartThread()
    {
        Thread thread = new Thread(() => ReadFile(filePath, itemsPerIteration, millisecondDelay));
        threads.Add(thread);
        thread.Name = threads.Count().ToString();
        thread.Start();
    }

    public static void ReadFile(string filePath, int itemsPerIteration, int millisecondDelay)
    {
        using StreamReader sr = new StreamReader(filePath);
        while (sr.Peek() != -1)
        {
            var resStr = "";
            for (int i = 0; i < itemsPerIteration && sr.Peek() != -1; i++)
                resStr += (char)sr.Read();

            Console.WriteLine($"Thread {Thread.CurrentThread.Name}: {resStr}");
            Thread.Sleep(millisecondDelay);
        }
    }

    private static void StopThread()
    {
        threads[^1].Abort();
        threads.RemoveAt(threads.Count - 1);
    }
}
