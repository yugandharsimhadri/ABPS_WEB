using ABPS_WEB.Domain.Lookups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's DataCache — the React client calls this once after login
/// and caches the result client-side, mirroring DataCache.Initialize()'s "load everything once"
/// behavior.</summary>
[ApiController]
[Authorize]
[Route("api/lookups")]
public class LookupsController : ControllerBase
{
    private readonly ILookupService _lookups;

    public LookupsController(ILookupService lookups) => _lookups = lookups;

    [HttpGet]
    public async Task<ActionResult<LookupsResponse>> GetAll() => Ok(await _lookups.GetAllAsync());
}
