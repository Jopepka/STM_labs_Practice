using Domain.Models;

namespace Application.Services;

public interface IBuildingService
{
    public Task<IEnumerable<Building>> GetAllBuildingsAsync();
}
