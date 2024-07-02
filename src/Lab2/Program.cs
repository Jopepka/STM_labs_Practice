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
        
        Console.WriteLine("Задание 6. Двусвязный список");
        MyList<int> myList = new MyList<int>([1, 2, 3, 4]);
        Console.WriteLine($"Изначальный список: {myList}");

        Console.WriteLine($"Получение по индексу 0: {myList[0]}");

        myList.PushLast(5);
        Console.WriteLine($"Вставка в конец 5: {myList}");

        myList.PushFirst(0);
        Console.WriteLine($"Вставка в начало 0: {myList}");

        myList.Insert(2, 100);
        Console.WriteLine($"Вставка в произвольное место: {myList}");

        myList[2] = 50;
        Console.WriteLine($"Изменение значения: {myList}");
    }
}
