#include "config.h"
#include "registration_client.h"

RegistrationClient registrationClient;

void setup()
{
    // Initialize serial
    Serial.begin(115200);
    delay(100);
    Serial.println("Starting...");

    // Init wifi
    WiFi.begin(wifi_ssid, wifi_password);
    Serial.println("Connecting");
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        Serial.print(".");
    }
    Serial.println("");
    Serial.print("Connected to WiFi network with IP Address: ");
    Serial.println(WiFi.localIP());

    registrationClient.Init();
    // registrationClient.Clear();

    // Check if device is registered
    if (registrationClient.CheckIfRegistered() == false)
    {
        Serial.println("Device id IS NOT set");

        // Wait for serial input
        while (Serial.available() == 0)
        {
            delay(100);
        }

        // Read serial input, ex: ABCD123,TOKEN;http://192.186.10.245:5009
        // String input = "test;test;http://192.186.10.245:7261";
        String input = Serial.readStringUntil('\n');

        int separatorIndex = input.indexOf(';');

        String deviceId = input.substring(0, separatorIndex);

        input = input.substring(separatorIndex + 1);

        separatorIndex = input.indexOf(';');

        String accessToken = input.substring(0, separatorIndex);

        String provisioningUrl = input.substring(separatorIndex + 1);

        // Register device id
        registrationClient.Register(deviceId, accessToken, provisioningUrl);
    }
    else
    {
        Serial.println("Device id IS set");

        // Print device id
        Serial.print("Device id: ");
        Serial.println(registrationClient.GetDeviceId());

        // Print access token
        Serial.print("Access token: ");
        Serial.println(registrationClient.GetAccessToken());

        // Print provisioning url
        Serial.print("Provisioning url: ");
        Serial.println(registrationClient.GetProvisioningUrl());
    }
}

void loop() {}