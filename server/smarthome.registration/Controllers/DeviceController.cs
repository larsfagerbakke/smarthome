using Microsoft.AspNetCore.Mvc;
using smarthome.shared.Services;

namespace smarthome.server.registration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
    public record RegisterDeviceV1Request(string DeviceIdentifier);
    public record RegisterDeviceV1Respond(string DeviceId, string ProvisioningDataAddress, string ProvisioningAccessToken);

    private readonly IConfiguration _configuration;
    private readonly ILogger<DeviceController> _logger;
    private readonly IDeviceService _deviceService;
    private readonly IDeviceTokenService _tokenService;

    public DeviceController(
        IConfiguration configuration,
        ILogger<DeviceController> logger,
        IDeviceService deviceService,
        IDeviceTokenService tokenService)
    {
        _configuration = configuration;
        _logger = logger;
        _deviceService = deviceService;
        _tokenService = tokenService;
    }

    [HttpPost("v1/Register")]
    public async Task<IActionResult> RegisterWithoutEncrytion(RegisterDeviceV1Request request)
    {
        // Generate a random device id
        var registrationResult = await _deviceService.RegisterV1Async(new RegisterV1Request
        {
            DeviceIdentifier = request.DeviceIdentifier
        });

        // Fetch data server address from configuration
        var provisioningServerAddress = _configuration["ProvisioningServerAddress"];

        // Generate provisioning token
        var provisioningAccessToken = await _tokenService.GenerateDeviceTokenV1Async(new GenerateDeviceTokenV1Request { DeviceId = registrationResult.DeviceId });

        return Ok(new RegisterDeviceV1Respond(registrationResult.DeviceId, provisioningServerAddress, provisioningAccessToken.Token));
    }
}
