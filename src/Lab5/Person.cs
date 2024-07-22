using System.Text.Json.Serialization;

public class Person
{
    public string? Name { get; set; }

    [JsonPropertyName("CountSSSS")]
    public int[] Nums { get; set; } = [1, 2, 3, 3];

    public int Age { get; set; }

    public Work? WorkPlace { get; set; }
}