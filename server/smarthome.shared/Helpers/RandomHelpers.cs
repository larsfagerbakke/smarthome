using System.Text;

namespace smarthome.shared.Helpers;

public class RandomHelpers
{
    public static string GenerateRandomDeviceId(int length = 8) => GenerateRandomString(length);

    public static string GenerateRandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(characters.Length);
            stringBuilder.Append(characters[index]);
        }

        return stringBuilder.ToString();
    }
}
