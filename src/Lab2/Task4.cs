public static class Task4
{
    public static int GetCountOverlap(string[] set1, string[] set2)
    {
        Dictionary<string, int> set1Dict = new Dictionary<string, int>();
        foreach (var item in set1)
            set1Dict[item] = 0;

        return set1Dict.Where(keyValue => set2.Contains(keyValue.Key)).Count();
    }
}