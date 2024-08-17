namespace Domain.Models;

public class Company
{
    public int Id { get; set; }
    public string CompanyName { get; set; }

    public int BuildingId { get; set; }
    public Building Building { get; set; }

    public ICollection<CompanyCategory> CompanyCategories { get; set; }
}
