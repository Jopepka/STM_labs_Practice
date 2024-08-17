using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BuildingRepository : IBuildingRepository
{
    private readonly ApplicationContext _context;

    public BuildingRepository(ApplicationContext context) => _context = context;

    public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
    {
        return await _context.Set<Building>().ToArrayAsync();
    }
}
