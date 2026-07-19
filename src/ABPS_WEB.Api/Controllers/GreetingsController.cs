using ABPS_WEB.Domain.Greetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's SendSmsWindow.xaml.cs (Send Greetings screen).</summary>
[ApiController]
[Authorize]
[Route("api/greetings")]
public class GreetingsController : ControllerBase
{
    private readonly IGreetingsService _greetings;

    public GreetingsController(IGreetingsService greetings) => _greetings = greetings;

    [HttpGet("sent-today-count")]
    public async Task<ActionResult<int>> GetSentTodayCount() => Ok(await _greetings.GetSentTodayCountAsync());

    [HttpGet("recipients/by-date")]
    public async Task<ActionResult<List<GreetingRecipient>>> FindByEnglishDate([FromQuery] DateTime date) =>
        Ok(await _greetings.FindByEnglishDateAsync(date));

    [HttpGet("recipients/by-telugu-calendar")]
    public async Task<ActionResult<List<GreetingRecipient>>> FindByTeluguCalendar(
        [FromQuery] int tMasamId, [FromQuery] int tPakshamId, [FromQuery] int tThiduluId) =>
        Ok(await _greetings.FindByTeluguCalendarAsync(tMasamId, tPakshamId, tThiduluId));

    [HttpPost("send")]
    public async Task<ActionResult<SendGreetingsResult>> Send(SendGreetingsRequest request) =>
        Ok(await _greetings.SendGreetingsAsync(request));
}
