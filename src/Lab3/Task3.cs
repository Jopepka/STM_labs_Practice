public class Task3
{
    public static Func<double, double> GetQuadraticEquation(double a, double b, double c) =>
        (double x) => a * Math.Pow(x, 2) + b * x + c;
}