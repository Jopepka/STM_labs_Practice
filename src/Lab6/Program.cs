using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Task PLINQ");
        // У меня выдает при последовательном 420мс, при параллельном 150мс
        new PlinqExample().Start();
    }
}
