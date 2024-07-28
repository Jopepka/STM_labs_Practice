class Program
{
    static void Main()
    {
        Console.WriteLine("Task PLINQ");
        // У меня выдает при последовательном ~250-340мс, при параллельном ~150-180мс
        new PlinqExample().Start();
    }
}
