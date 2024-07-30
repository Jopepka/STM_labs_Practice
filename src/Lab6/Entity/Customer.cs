public class Customer
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public City? City { get; set; }

    public override string ToString() => $"{ID}. {Name}, city: {City}";
}