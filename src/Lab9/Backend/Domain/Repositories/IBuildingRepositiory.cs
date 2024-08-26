using Domain.Models;

namespace Domain.Repositories;

public interface IBuildingRepository
{
    Task<IEnumerable<Building>> GetAllBuildingsAsync();
}