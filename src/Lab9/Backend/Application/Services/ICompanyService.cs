using Domain.Models;

namespace Application.Services;

public interface ICompanyService
{
    Task<Company> GetCompanyById(int companyId);

    Task<IEnumerable<Company>> GetCompaniesInBuildingAsync(int buildingId);

    Task<IEnumerable<Company>> GetCompaniesInCategoryAsync(int categoryId);

    Task<IEnumerable<Company>> GetCompaniesNearLocationAsync(double latitude, double longitude, double radius);

    Task<IEnumerable<Company>> GetCompaniesByNameAsync(string name);
}