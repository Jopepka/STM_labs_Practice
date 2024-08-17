namespace Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public int? ParentCategoryId { get; set; }
}
