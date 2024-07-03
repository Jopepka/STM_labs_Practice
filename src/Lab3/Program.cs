namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 4. Слушатель и публикация событий");
        var publisher1 = new Task4_Publisher("Publisher 1");
        var publisher2 = new Task4_Publisher("Publisher 2");

        var listener = new Task4_Listener();
        publisher1.Subscribe(listener.ListenObject);
        publisher2.Subscribe(listener.ListenObject);

        publisher1.RaiseEvent();
        publisher2.RaiseEvent();
    }
}
