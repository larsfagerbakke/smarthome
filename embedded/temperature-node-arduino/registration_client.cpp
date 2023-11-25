#include "registration_client.h"

RegistrationClient::RegistrationClient() {}

RegistrationClient::~RegistrationClient() {}

bool RegistrationClient::Init()
{
    EEPROM.begin(EEPROM_SIZE);

    return true;
}

bool RegistrationClient::CheckIfRegistered()
{
    uint8_t deviceIdFlag = EEPROM.read(0);

    if (deviceIdFlag != 0xFF)
    {
        return false;
    }
    else
    {
        return true;
    }
}

bool RegistrationClient::Register(String deviceId, String accessToken, String provisioningUrl)
{
    // Write device id to serial
    Serial.print("Registering device id: ");
    Serial.println(deviceId);

    // Write 8 bytes from input to EEPROM
    for (int i = 0; i < 8; i++)
    {
        EEPROM.write(i + 1, deviceId[i]);
        delay(100);
    }

    // Write access token to serial
    Serial.print("Registering access token: ");
    Serial.println(accessToken);

    // Get access token byte length
    int accessTokenLength = accessToken.length();

    // Write access token length to serial
    Serial.print("Registering access token length: ");
    Serial.println(accessTokenLength);

    // Write access token length to EEPROM
    EEPROM.write(9, (byte)accessTokenLength);
    delay(100);

    // Write access token to EEPROM
    for (int i = 0; i < accessTokenLength; i++)
    {
        EEPROM.write(i + 10, accessToken[i]);
        delay(100);
    }

    // Write provisioning url to serial
    Serial.print("Registering provisioning url: ");
    Serial.println(provisioningUrl);

    // Get provisioning url byte length
    int provisioningUrlLength = provisioningUrl.length();

    // Write provisioning url length to serial
    Serial.print("Registering provisioning url length: ");
    Serial.println(provisioningUrlLength);

    // Write provisioning url length to EEPROM
    EEPROM.write(10 + accessTokenLength, (byte)provisioningUrlLength);
    delay(100);

    // Write provisioning url to EEPROM
    for (int i = 0; i < provisioningUrlLength; i++)
    {
        EEPROM.write(i + 11 + accessTokenLength, provisioningUrl[i]);
        delay(100);
    }

    // Write FF to EEPROM index 0, flagging that device id is set
    EEPROM.write(0, (byte)0xFF);
    delay(100);

    EEPROM.commit();
    delay(100);

    return true;
}

bool RegistrationClient::Clear()
{
    // Write FF to EEPROM index 0, flagging that device id is not set
    EEPROM.write(0, (byte)0x00);
    delay(100);

    EEPROM.commit();
    delay(100);

    return true;
}

String RegistrationClient::GetDeviceId()
{
    // Read 8 bytes from EEPROM from index 1
    char deviceId[8];
    for (int i = 0; i < 8; i++)
    {
        deviceId[i] = EEPROM.read(i+1);
    }

    return String(deviceId);
}

String RegistrationClient::GetAccessToken()
{
    // Read access token length from EEPROM index 9
    int accessTokenLength = EEPROM.read(9);

    // Read access token from EEPROM from index 10
    char accessToken[accessTokenLength];
    for (int i = 0; i < accessTokenLength; i++)
    {
        accessToken[i] = EEPROM.read(i + 10);
    }

    return String(accessToken);
}

String RegistrationClient::GetProvisioningUrl()
{
    // Read access token length from EEPROM index 9
    int accessTokenLength = EEPROM.read(9);

    // Read provisioning url length from EEPROM index 9
    int provisioningUrlLength = EEPROM.read(accessTokenLength + 1 + 9);

    // Read provisioning url from EEPROM from index 10
    char provisioningUrl[provisioningUrlLength];
    for (int i = 0; i < provisioningUrlLength; i++)
    {
        provisioningUrl[i] = EEPROM.read(i + 10);
    }

    return String(provisioningUrl);
}