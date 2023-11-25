#ifndef PROVISIONING_CLIENT_H
#define PROVISIONING_CLIENT_H

#include <Preferences.h>

class ProvisioningClient
{
public:
    ProvisioningClient();
    ~ProvisioningClient();

    bool Init();
    bool CheckIfRegistered();
    bool Register(String deviceId, String provisioningUrl);
    bool Clear();

    String GetDeviceId();
    String GetProvisioningUrl();
};

#endif // PROVISIONING_CLIENT_H