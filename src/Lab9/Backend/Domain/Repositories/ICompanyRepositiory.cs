using Domain.Models;

namespace Domain.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesInBuildingAsync(int buildingId);

    Task<IEnumerable<Company>> GetCompaniesInCategoryAsync(int categoryId);

    Task<IEnumerable<Company>> GetCompaniesNearLocationAsync(double latitude, double longitude, double radius);

    Task<IEnumerable<Company>> GetCompaniesByNameAsync(string name);

    Task<Company> GetByIdAsync(int companyId);
}
