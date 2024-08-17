using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        var company = await _companyService.GetCompanyById(id);
        return company == null ? NotFound() : Ok(company);
    }

    [HttpGet("ByBuilding/{buildingId}")]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesByBuildingId(int buildingId)
    {
        var companies = await _companyService.GetCompaniesInBuildingAsync(buildingId);
        return Ok(companies);
    }

    [HttpGet("ByCategory/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesByCategoryId(int categoryId)
    {
        var companies = await _companyService.GetCompaniesInCategoryAsync(categoryId);
        return Ok(companies);
    }

    [HttpGet("InArea")]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesInArea(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] double radius)
    {
        var companies = await _companyService.GetCompaniesNearLocationAsync(latitude, longitude, radius);
        return Ok(companies);
    }

    [HttpGet("SearchByCategory/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Company>>> SearchCompaniesByCategory(int categoryId)
    {
        var companies = await _companyService.GetCompaniesInCategoryAsync(categoryId);
        return Ok(companies);
    }

    [HttpGet("SearchByName/{name}")]
    public async Task<ActionResult<IEnumerable<Company>>> SearchCompaniesByName(string name)
    {
        var companies = await _companyService.GetCompaniesByNameAsync(name);
        return Ok(companies);
    }
}
