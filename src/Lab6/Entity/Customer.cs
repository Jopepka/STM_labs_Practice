public class Customer
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int CityID { get; set; }

    public override string ToString() => $"{ID}. {Name}, city: {CityID}";
}