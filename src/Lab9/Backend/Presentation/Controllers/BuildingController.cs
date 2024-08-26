using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Building>>> GetAllBuildings()
    {
        var buildings = await _buildingService.GetAllBuildingsAsync();
        return buildings == null ? NoContent() : Ok(buildings);
    }
}
