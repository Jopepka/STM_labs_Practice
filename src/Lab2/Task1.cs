using System.Numerics;

public static class Task1
{
    public static T SumMinAndMax<T>(T number1, T number2, T number3) where T : INumber<T>
    {
        T[] nums = [number1, number2, number3];
        return nums.Min() + nums.Max();
    }
}