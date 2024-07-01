namespace STM_labs_Practice;

class Program
{
    static void Main(string[] args)
    {
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
