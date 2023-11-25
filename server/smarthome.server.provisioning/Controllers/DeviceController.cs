using Microsoft.AspNetCore.Mvc;
using smarthome.shared.Services;

namespace smarthome.provisioning.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
    public record ProvisioningDeviceV1Request(string deviceId, string token, string publicDeviceSertificate);
    public record class ProvisioningDeviceV1Response(string publicServerCertificate, string dataServerAddress);

    private readonly IConfiguration _configuration;
    private readonly ILogger<DeviceController> _logger;
    private readonly IDeviceTokenService _deviceTokenService;

    public DeviceController(
        IConfiguration configuration,
        ILogger<DeviceController> logger,
        IDeviceTokenService devicetokenService)
    {
        _logger = logger;
        _deviceTokenService = devicetokenService;
    }

    [HttpPost("v1/Provisioning")]
    public async Task<IActionResult> ProvisioningWithoutEncrytion(ProvisioningDeviceV1Request request)
    {
        // Validate token for device
        var tokenRequest = await _deviceTokenService.ValidateTokenV1Async(new ValidateTokenV1Request { DeviceId = request.deviceId, Token = request.token });

        if (!tokenRequest.Success) return Unauthorized();

        // Generate server-device certificate
        var certificate = string.Empty;         // TODO, not needed since data is not encrypted. Just placeholder
        var publicKey = string.Empty;           // TODO, not needed since data is not encrypted. Just placeholder

        // Fetch data server address from configuration
        var dataServerAddress = _configuration["DataServerAddress"];

        return Ok(new ProvisioningDeviceV1Response(dataServerAddress, publicKey));
    }

    [HttpPost("v2/Provisioning")]
    public IActionResult ProvisioningWithAES()
    {
        // Generate a random device identifier

        // Generate a random AES key

        // Return the device identifier and the AES key

        throw new NotImplementedException();
    }

    [HttpPost("v3/Provisioning")]
    public IActionResult ProvisioningWithRSA()
    {
        // Generate a random device identifier

        // Generate a random RSA key pair

        // Store device public key in database

        // Return the device identifier and the server public key

        throw new NotImplementedException();
    }
}
