using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

public record FeatureFlagsResponse(bool CameraCaptureEnabled, bool DocumentUploadEnabled);

/// <summary>Ported from ABPS_WPF's Helper/FeatureFlags.cs — same fail-closed default (missing/
/// blank/anything-but-true is disabled). WPF's flags only ever gated UI visibility (the
/// Capture Photo / Upload Document buttons), never the underlying save logic itself, so this
/// endpoint is the React client's equivalent of BookRoomWindow's constructor-time
/// `pnlCameraCapture.Visibility = FeatureFlags.IsEnabled(...) ? Visible : Collapsed` — the
/// upload endpoint itself (RoomsController.UploadDocument) stays ungated, matching that exact
/// behavior rather than adding a new server-side restriction.</summary>
[ApiController]
[Authorize]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;

    public ConfigController(IConfiguration config) => _config = config;

    [HttpGet("feature-flags")]
    public ActionResult<FeatureFlagsResponse> GetFeatureFlags()
    {
        bool camera = bool.TryParse(_config["FeatureFlags:CameraCaptureEnabled"], out var c) && c;
        bool docUpload = bool.TryParse(_config["FeatureFlags:DocumentUploadEnabled"], out var d) && d;
        return Ok(new FeatureFlagsResponse(camera, docUpload));
    }
}
