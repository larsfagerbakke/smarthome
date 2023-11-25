# Smarthome Registration

This is the device registration server. There should be a companion application that registers a success flashed device. 

The process is typical done by a production partner. During production it will flash the device and fetch a serial/identity number. Different chips solves this in a different way. Some have serial number. ESP32 don't have this, but it has MAC. Not that secure, but hey - it will serve it's purpose.

## The process

The process is as follows:
- Flash device
- Query device for serial/identity number
- Make a secure register request (POST) to the system with production data and serial/identity number alongside with a system security token
- The system will return a device id (ABC123) and system security token
 
## Encryption

For security both server and node should generate and exchange public keys, and each keep their private keys.

Arduino and/or ESP32 is not the most secure platform, and somewhat limited. First issue will not handle encryption at all, before we move up to symetrical encryption (AES) before we will try asymetrical encryption (RSA).
