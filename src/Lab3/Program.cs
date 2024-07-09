namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
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
    }
}
