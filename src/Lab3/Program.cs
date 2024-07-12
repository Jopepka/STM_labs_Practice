namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задане 1. Калькулятор");
        ConsoleCalculator.Run();

        Console.WriteLine("Задание 2. Дни недели");
        var dayOWeek = Task2.GetDayOWeekFunc();
        Console.WriteLine(dayOWeek()); // Понедельник
        Console.WriteLine(dayOWeek()); // Вторник
        Console.WriteLine(dayOWeek()); // Среда
        Console.WriteLine(dayOWeek()); // Четверг
        Console.WriteLine(dayOWeek()); // Пятница
        Console.WriteLine(dayOWeek()); // Суббота
        Console.WriteLine(dayOWeek()); // Воскресень
        Console.WriteLine(dayOWeek()); // Понедельник
        Console.WriteLine(dayOWeek()); // Вторник

        Console.WriteLine("Задание 3. Квадратичный трехчлен");
        var quadraticEquation = Task3.GetQuadraticEquation(1, 2, 3); // 1*x^2 + 2*x + 3 
        Console.WriteLine(quadraticEquation(2)); // 11
        
        Console.WriteLine("Задание 4. Слушатель и публикация событий");
        var publisher1 = new Task4_Publisher("Publisher 1");
        var publisher2 = new Task4_Publisher("Publisher 2");

        var listener = new Task4_Listener();
        publisher1.Subscribe(listener.ListenObject);
        publisher2.Subscribe(listener.ListenObject);

        publisher1.RaiseEvent();
        publisher2.RaiseEvent();
      
        Console.WriteLine("Задание 5. Чтение файла");
        LimitedStringLoader a = new LimitedStringLoader("de", "htp", 2);
        a.Load("task5_testData.txt");
        Console.WriteLine(string.Join("\n", a.LoadedLines));
    }

}
