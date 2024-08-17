using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationContext _context;
    public CompanyRepository(ApplicationContext appContext) => _context = appContext;

    public async Task<IEnumerable<Company>> GetCompaniesInBuildingAsync(int buildingId)
    {
        return await _context.Companies.Where(company => company.BuildingId == buildingId).ToArrayAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesInCategoryAsync(int categoryId)
    {
        return await _context.Companies.Where(company =>
            company.CompanyCategories.Any(category => category.CategoryId == categoryId))
            .ToArrayAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesNearLocationAsync(double latitude, double longitude, double radius)
    {
        return await _context.Companies.Where(company =>
            Math.Sqrt(Math.Pow(company.Building.Latitude - latitude, 2) + Math.Pow(company.Building.Longitude - longitude, 2)) <= radius)
            .ToListAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesByNameAsync(string name)
    {
        return await _context.Companies
            .Where(company => EF.Functions.Like(company.CompanyName, $"%{name}%"))
            .ToListAsync();
    }
}
