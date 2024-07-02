namespace STM_labs_Practice;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 1. Сумма минимального и максимального.");
        Console.WriteLine(Task1.SumMinAndMax(2, 10, 7));
      
        Console.WriteLine("Задание 2. Количество амеб через 3, 6, ... 24 часа");
        Console.WriteLine(string.Join(", ", Task2.GetCountAmebs()));
      
        Console.WriteLine("Задание 4. Количество общих элементов множества");
        Console.WriteLine(Task4<int>.GetCountOverlap([1, 2, 3], [2, 3, 4]));
    }
}
