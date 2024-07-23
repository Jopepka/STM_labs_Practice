using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MyJsonDeserializer
{
    public static T DeserializeFromJson<T>(string jsonString)
    {
        try
        {
            return (T)DeserializeObject(JsonDocument.Parse(jsonString).RootElement, typeof(T));
        }
        catch
        {
            throw new ArgumentException("Неверный формат json");
        }
    }

    static object DeserializeObject(JsonElement element, Type type)
    {
        if (element.ValueKind == JsonValueKind.Null)
            return null;

        if (TryDeserializePrimitive(element, type, out object value))
            return value;

        if (type.IsEnum)
            return Enum.Parse(type, element.GetString());

        if (type.IsArray)
            return DeserializeArray(element, type);

        return DeserializeComplexObject(element, type);
    }

    static bool TryDeserializePrimitive(JsonElement element, Type type, out object value)
    {
        value = null;
        if (type == typeof(string))
            value = element.GetString();
        else if (type == typeof(int))
            value = element.GetInt32();
        else if (type == typeof(float))
            value = element.GetSingle();
        else if (type == typeof(double))
            value = element.GetDouble();
        else if (type == typeof(bool))
            value = element.GetBoolean();
        else if (type == typeof(decimal))
            value = element.GetDecimal();
        else
            return false;
        return true;
    }

    static object DeserializeArray(JsonElement element, Type type)
    {
        IList list = Array.CreateInstance(type.GetElementType(), element.GetArrayLength());

        int index = 0;
        foreach (var item in element.EnumerateArray())
            list[index++] = DeserializeObject(item, type.GetElementType());

        return list;
    }

    static object DeserializeComplexObject(JsonElement element, Type type)
    {
        var obj = Activator.CreateInstance(type);
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                continue;

            var name = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;

            if (element.TryGetProperty(name, out JsonElement propertyElement))
                property.SetValue(obj, DeserializeObject(propertyElement, property.PropertyType));
        }
        return obj;
    }
}
