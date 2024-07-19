using System.Text.Json;

internal class FileLoader
{
    public static IEnumerable<T>? Load<T>(string _fileName)
    {
        string jsonString = File.ReadAllText(_fileName);
        var items = jsonString == "" ? null : JsonSerializer.Deserialize<T[]>(jsonString);
        return items;
    }

    public static void Save<T>(IEnumerable<T> items, string fileName)
    {
        Console.WriteLine(items.Count());
        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(fileName, json);
    }
}