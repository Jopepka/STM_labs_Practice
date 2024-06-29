public static class Task1
{
    public static (double, double) getMinAndMax(double number1, double number2, double number3)
    {
        List<double> nums = [number1, number2, number3];
        nums.Sort();
        return (nums[0], nums[2]);
    }
}