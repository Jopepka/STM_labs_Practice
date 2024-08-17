using Domain.Models;
using Domain.Repositories;

namespace Application.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _buildingRepository;

    public BuildingService(IBuildingRepository buildingRepository) => _buildingRepository = buildingRepository;

    public Task<IEnumerable<Building>> GetAllBuildingsAsync()
    {
        return _buildingRepository.GetAllBuildingsAsync();
    }
}
