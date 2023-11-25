namespace smarthome.shared.Models;

public class Device
{
    public enum DeviceType
    {
        TemperatureSensor = 1
    }

    public string Id { get; set; }
    public string Identifier { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Name { get; set; }
    public DeviceType Type { get; set; }
    public string PublicKey { get; set; }
}
