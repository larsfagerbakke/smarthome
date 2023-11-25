using ProtoBuf.Grpc;
using smarthome.shared.Helpers;
using smarthome.shared.Models;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace smarthome.shared.Services;

[DataContract]
public record GenerateDeviceTokenV1Request
{
    [DataMember(Order = 1)]
    public string DeviceId { get; set; }

    [DataMember(Order = 2)]
    public DateTime? Expire { get; set; }
}

[DataContract]
public record GenerateDeviceTokenV1Response
{
    [DataMember(Order = 1)]
    public string Token { get; set; }
}

[DataContract]
public record ValidateTokenV1Request
{
    [DataMember(Order = 1)]
    public string DeviceId { get; set; }

    [DataMember(Order = 2)]
    public string Token { get; set; }
}

[DataContract]
public record ValidateTokenV1Response
{
    [DataMember(Order = 1)]
    public bool Success { get; set; }
}


[ServiceContract]
public interface IDeviceTokenService
{
    /// <summary>
    /// Generates a token for a device. Function should also store the token for later validation.
    /// </summary>
    /// <param name="deviceId">Device id</param>
    /// <param name="expires">Expiration date</param>
    /// <returns>Generated token</returns>
    [OperationContract]
    Task<GenerateDeviceTokenV1Response> GenerateDeviceTokenV1Async(GenerateDeviceTokenV1Request request, CallContext context = default);

    /// <summary>
    /// Validates a token for a device. Function should also remove the token from storage.
    /// </summary>
    /// <param name="deviceId">Device id</param>
    /// <param name="token">Token to validate</param>
    /// <returns>TRUE if token matches and if validated</returns>
    [OperationContract]
    Task<ValidateTokenV1Response> ValidateTokenV1Async(ValidateTokenV1Request request, CallContext context = default);
}

public class InMemoryDeviceTokenService : IDeviceTokenService
{
    private readonly List<DeviceToken> _deviceTokens = new();

    public InMemoryDeviceTokenService()
    {
        _deviceTokens.Add(new DeviceToken
        {
            DeviceId = "test",
            Token = "test",
            Expires = DateTime.UtcNow.AddHours(12)
        });
    }

    public async Task<GenerateDeviceTokenV1Response> GenerateDeviceTokenV1Async(GenerateDeviceTokenV1Request request, CallContext context = default)
    {
        // Check if device already has a token
        if (_deviceTokens.Any(x => x.DeviceId == request.DeviceId))
        {
            throw new InvalidOperationException("Device already has a token");
        }

        // Generate new token
        var token = RandomHelpers.GenerateRandomString(12);

        // Set expiration
        if (request.Expire == null)
        {
            request.Expire = DateTime.UtcNow.AddHours(1);
        }

        // Store token in memory
        _deviceTokens.Add(new DeviceToken
        {
            DeviceId = request.DeviceId,
            Token = token,
            Expires = request.Expire.Value
        });

        return new GenerateDeviceTokenV1Response
        {
            Token = token
        };
    }

    public async Task<ValidateTokenV1Response> ValidateTokenV1Async(ValidateTokenV1Request request, CallContext context = default)
    {
        // Fetch token from memory
        var deviceToken = _deviceTokens.FirstOrDefault(x => x.DeviceId == request.DeviceId);

        var storedToken = deviceToken.Token;
        var storedTokenExpiration = deviceToken.Expires;
        var storedDeviceId = deviceToken.DeviceId;

        // Remove token
        _deviceTokens.Remove(deviceToken);

        // Check if token is valid
        if (deviceToken == null) return new ValidateTokenV1Response { Success = false };

        // Check if token is expired
        if (deviceToken.Expires < DateTime.UtcNow) return new ValidateTokenV1Response { Success = false };

        // Check if token matches
        if (deviceToken.Token == request.Token) return new ValidateTokenV1Response { Success = true };

        return new ValidateTokenV1Response { Success = false };
    }
}
