using System.Numerics;

public static class Task1<T> where T : INumber<T>
{
    public static T SumMinAndMax(T number1, T number2, T number3)
    {
        T[] nums = [number1, number2, number3];
        return nums.Min() + nums.Max();
    }
}