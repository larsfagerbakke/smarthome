#include "provisioning_client.h"
#include "registration_client.h"

RegistrationClient registrationClient;

ProvisioningClient::ProvisioningClient() {}

ProvisioningClient::~ProvisioningClient() {}

ProvisioningClient::Provisioning()
{
    // Get device id from Registration client
    String deviceId = registrationClient.GetDeviceId();

    // Get token from Registration client
    String token = registrationClient.GetAccessToken();

    // Get provisioning url from Registration client
    String provisioningUrl = registrationClient.GetProvisioningUrl();

    String publicKey = "";
}