public static class Task5
{
    public static int CountDays(DateTime date1, DateTime date2)
    {
        return Math.Abs((date1 - date2).Days);
    }
}