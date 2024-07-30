public class City
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int CityCode { get; set; }

    public override string ToString() => $"{ID}. {Name}, cityPhone: {CityCode}";
}