public static class Task4
{
    public static int GetCountOverlap<T>(IEnumerable<T> plenty1, IEnumerable<T> plenty2)
    {
        CheckCorrectParams(plenty1, plenty2);
        return plenty1.Count(key => plenty2.Contains(key));
    }

    private static void CheckCorrectParams<T>(IEnumerable<T> plenty1, IEnumerable<T> plenty2)
    {
        CheckNotNull(plenty1);
        CheckNotNull(plenty2);

        CheckOnlyUniqluItems(plenty1);
        CheckOnlyUniqluItems(plenty2);
    }

    private static void CheckNotNull(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException();
    }

    private static void CheckOnlyUniqluItems<T>(IEnumerable<T> collection)
    {
        if (collection.Count() != collection.Distinct().Count())
            throw new ArgumentException("The collection must contain unique values");
    }
}