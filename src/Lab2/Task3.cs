public static class Task3
{
    public static int CalculateArrows(string text)
    {
        string arrow1 = ">>-->";
        string arrow2 = "<--<<";

        return FindCountSubstrings(arrow1, text) + FindCountSubstrings(arrow2, text);
    }

    private static int FindCountSubstrings(string substring, string text)
    {
        int count = 0;
        int indexOf = text.IndexOf(substring, 0);
        while (indexOf != -1)
        {
            count++;
            indexOf = text.IndexOf(substring, indexOf + substring.Length);
        }
        return count;
    }
}