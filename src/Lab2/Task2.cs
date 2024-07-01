public static class Task2
{
    public static IEnumerable<int> GetCountAmebs()
    {
        int[] countAmebs = new int[24 / 3];
        for (int i = 0; i < 24 / 3; i++)
            countAmebs[i] = (int)Math.Pow(2, i + 1);

        return countAmebs;
    }
}