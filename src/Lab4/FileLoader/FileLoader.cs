using System.Text.Json;

internal class FileLoader
{
    public static IEnumerable<T>? Load<T>(string _fileName)
    {
        string jsonString = File.ReadAllText(_fileName);
        var items = JsonSerializer.Deserialize<T[]>(jsonString);
        return items;
    }

    public static void Save<T>(Dictionary<int, T> items, string fileName) =>
        File.WriteAllText(fileName, JsonSerializer.Serialize(items.ToArray()));
}