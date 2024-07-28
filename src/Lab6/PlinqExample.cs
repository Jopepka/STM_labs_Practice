using System.Diagnostics;

public class PlinqExample
{
    public int numPoints { get; set; } = 10000000;
    public Random random { get; set; } = new Random(41);

    readonly Stopwatch _stopwatch = new Stopwatch();

    public void Start()
    {
        var points = Enumerable.Range(0, numPoints).Select(i => new { x = random.NextDouble(), y = random.NextDouble() }).ToArray();

        _stopwatch.Restart();
        int countSequential = points.Count(item => IsPointInCircle(item.x, item.y));
        _stopwatch.Stop();

        Console.WriteLine($"Последовательное вычисление числа Пи: {CalculatePi(countSequential)}");
        Console.WriteLine($"Время последовательного выполнения: {_stopwatch.ElapsedMilliseconds} мс");

        _stopwatch.Restart();
        int countParallel = points.AsParallel().Count(item => IsPointInCircle(item.x, item.y));
        _stopwatch.Stop();

        Console.WriteLine($"Параллельное вычисление числа Пи: {CalculatePi(countParallel)}");
        Console.WriteLine($"Время параллельного выполнения: {_stopwatch.ElapsedMilliseconds} мс");
    }

    private bool IsPointInCircle(double x, double y) => x * x + y * y <= 1;

    private double CalculatePi(int countPointsInCircle) => 4.0 * countPointsInCircle / numPoints;
}