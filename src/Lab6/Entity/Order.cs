public class Order
{
    public int ID { get; set; }
    public Customer? Customer { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public override string ToString() => $"{ID}. customer: {Customer}, price: {Price}, Date: {Date}";
}