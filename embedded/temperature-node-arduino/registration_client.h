#ifndef REGISTRATION_CLIENT_H
#define REGISTRATION_CLIENT_H

#include <EEPROM.h>

#define EEPROM_SIZE 50

class RegistrationClient
{
public:
    RegistrationClient();
    ~RegistrationClient();

    bool Init();
    bool CheckIfRegistered();
    bool Register(String deviceId, String accessToken, String provisioningUrl);
    bool Clear();

    String GetDeviceId();
    String GetAccessToken();
    String GetProvisioningUrl();
};

#endif // REGISTRATION_CLIENT_H