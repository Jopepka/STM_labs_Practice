public class Order
{
    public int ID { get; set; }
    public int CustomerID { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public override string ToString() => $"{ID}. customer: {CustomerID}, price: {Price}, Date: {Date}";
}