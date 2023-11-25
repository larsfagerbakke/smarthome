using ProtoBuf.Grpc;
using smarthome.shared.Helpers;
using smarthome.shared.Models;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace smarthome.shared.Services;

[DataContract]
public record RegisterV1Request
{
    [DataMember(Order = 1)]
    public string DeviceIdentifier { get; set; }
}

[DataContract]
public record RegisterV1Response
{
    [DataMember(Order = 1)]
    public string DeviceId { get; set; }

    [DataMember(Order = 2)]
    public string ProvisioningUrl { get; set; }

    [DataMember(Order = 3)]
    public string ProvisioningAccessToken { get; set; }
}

[DataContract]
public record SetDevicePublicKeyV1Request
{
    [DataMember(Order = 1)]
    public string DeviceId { get; set; }

    [DataMember(Order = 2)]
    public string PublicKey { get; set; }
}

[DataContract]
public record SetDevicePublicKeyV1Response
{
    [DataMember(Order = 1)]
    public bool Success { get; set; }
}

[ServiceContract]
public interface IDeviceService
{
    [OperationContract]
    Task<SetDevicePublicKeyV1Response> SetDevicePublicKeyV1Async(SetDevicePublicKeyV1Request request, CallContext context = default);

    [OperationContract]
    Task<RegisterV1Response> RegisterV1Async(RegisterV1Request request, CallContext context = default);
}

public class InMemoryDeviceService : IDeviceService
{
    private readonly List<Device> _devices = new();

    public InMemoryDeviceService()
    {
        // Add a test device
        _devices.Add(new Device
        {
            Id = "test",
            Identifier = "test",
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Type = Device.DeviceType.TemperatureSensor
        });
    }

    public async Task<RegisterV1Response> RegisterV1Async(RegisterV1Request request, CallContext context = default)
    {
        // Generate a random device identifier
        var id = RandomHelpers.GenerateRandomDeviceId();

        // Validate that there are no other devices with the same identifier
        while (_devices.Any(x => x.Identifier == id))
        {
            id = RandomHelpers.GenerateRandomDeviceId();
        }

        _devices.Add(new Device
        {
            Id = id,
            Identifier = request.DeviceIdentifier,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Type = Device.DeviceType.TemperatureSensor
        });

        return new RegisterV1Response
        {
            DeviceId = id
        };
    }

    public async Task<SetDevicePublicKeyV1Response> SetDevicePublicKeyV1Async(SetDevicePublicKeyV1Request request, CallContext context = default)
    {
        // Get device
        var device = _devices.FirstOrDefault(x => x.Id == request.DeviceId);

        // Set public key
        device.PublicKey = request.PublicKey; // It's in memory so just set it

        return new SetDevicePublicKeyV1Response
        {
            Success = true
        };
    }
}
