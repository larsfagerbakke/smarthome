namespace smarthome.shared.Models;

public class DeviceToken
{
    public string DeviceId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}
