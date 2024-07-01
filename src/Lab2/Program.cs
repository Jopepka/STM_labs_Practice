namespace STM_labs_Practice;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 4. Количество общих элементов множества");
        Console.WriteLine(Task4<int>.GetCountOverlap([1, 2, 3], [2, 3, 4]));
    }
}
