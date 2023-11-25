# Arduino Temperature device

Designed to work with the ESP32-S2

## Registration client

Here are some function to check if the device is registeret or not, and to register the device.

Registration is done thru serial input as the device itself cant register, this is usually left to a production partner.

The device checks a flag and if not set it will wait for a string input (device id, ex: "ABCD1234"). Future work will inlclude exhange of encryption keys.

The module also includes a function to clear the flag so it can start the process from scratch.

## Missing config.h file

The file is missing due to it containing sensetive data. Use this template and fill inn correct addresses, wifi credentials and other:

```
#ifndef CONFIG_H
#define CONFIG_H

const char* token = "token1234";

const char* publicKeyPEM = R"(-----BEGIN RSA PUBLIC KEY-----
MEgCQQCo9+BpMRYQ/dL3DS2CyJxRF+j6ctbT3/Qp84+KeFhnii7NT7fELilKUSnx
S30WAvQCCo2yU1orfgqr41mM70MBAgMBAAE=
-----END RSA PUBLIC KEY-----)";

const char* wifi_ssid = "WIFI_SSID";
const char* wifi_password = "WIFI_PASS";

const char* server = "192.168.10.245";
const int port = 5136;

const char* tcpServer = "192.168.10.245";
const int tcpServerPort = 12345;

#endif // CONFIG_H
```

## TODO