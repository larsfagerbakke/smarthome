# Smarthome server applications

## Registration

Production partner uses this server to introduce devices to the system. Exchanges device production information, serial/identifying number and public key for device id and system public key. 

## Provisioning

2nd leg of getting a device online. The device uses endpoints to set up data transfer. TODO

## Server.HTTP(S)

Intial server application until TCP is implemented. Open, but data should be encrypted (TODO).

## Server.TCP

TODO

## Shared

TODO

### Services

As a first phase each of the servers will run their own services. This will put all executions against the database table on multiple servers making it possible for race conditions. But the services are created in a way that it should be easy to create gRPC services for them later, where each gRPC service is a microservice that owns one ore more tables, not allowing access to the data without going thru the service. But for now it's just mayhem, it is fine. 