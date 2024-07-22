using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;
using System.Text.Json.Serialization;

public class MyJsonSerializer
{
    public static string Serialize(object obj, int maxDepth = 64)
    {
        if (maxDepth == 0)
            throw new Exception();
        if (obj == null)
            return "null";
        else if (obj.GetType().IsArray)
            return SerializeArray((IEnumerable)obj, maxDepth);
        else if (obj.GetType() != typeof(string) && !obj.GetType().IsPrimitive)
            return SerializeObj(obj, maxDepth);
        else
            return obj.ToString();
    }

    private static string SerializeObj(object obj, int maxDepth)
    {
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Dictionary<string, string> items = new Dictionary<string, string>();

        foreach (var property in properties)
        {
            if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                continue;

            var name = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;
            items[name] = Serialize(property.GetValue(obj), maxDepth - 1);
        }

        return DictToString(items);
    }

    private static string DictToString(Dictionary<string, string> dict) =>
        "{" + string.Join(", ", dict.Select(keyValue => $"{keyValue.Key}: {keyValue.Value}")) + "}";

    private static string SerializeArray(IEnumerable array, int maxDepth)
    {
        var res = "[";
        var isFirst = true;
        foreach (var item in array)
        {
            if (!isFirst)
                res += ", ";
            res += Serialize(item, maxDepth - 1);
            isFirst = false;
        }
        return res + "]";
    }
}
