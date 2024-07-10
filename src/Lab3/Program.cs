namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 5. Чтение файла");
        LimitedStringLoader a = new LimitedStringLoader("de", "htp", 2);
        a.Load("task5_testData.txt");
        Console.WriteLine(string.Join("\n", a.LoadedLines));
    }
}
