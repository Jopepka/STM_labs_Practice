namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задание 3. Квадратичный трехчлен");
        var quadraticEquation = Task3.GetQuadraticEquation(1, 2, 3); // 1*x^2 + 2*x + 3 
        Console.WriteLine(quadraticEquation(2)); // 11
    }
}
