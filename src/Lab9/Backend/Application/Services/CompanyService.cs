using Domain.Models;
using Domain.Repositories;

namespace Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository) => _companyRepository = companyRepository;

    public Task<IEnumerable<Company>> GetCompaniesInBuildingAsync(int buildingId)
    {
        return _companyRepository.GetCompaniesInBuildingAsync(buildingId);
    }

    public Task<IEnumerable<Company>> GetCompaniesInCategoryAsync(int categoryId)
    {
        return _companyRepository.GetCompaniesInCategoryAsync(categoryId);
    }

    public Task<IEnumerable<Company>> GetCompaniesNearLocationAsync(double latitude, double longitude, double radius)
    {
        return _companyRepository.GetCompaniesNearLocationAsync(latitude, longitude, radius);
    }

    public Task<Company> GetCompanyById(int companyId)
    {
        return _companyRepository.GetByIdAsync(companyId);
    }

    public Task<IEnumerable<Company>> GetCompaniesByNameAsync(string name)
    {
        return _companyRepository.GetCompaniesByNameAsync(name);
    }
}