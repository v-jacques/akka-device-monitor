# akka-device-monitor

## Description

Akka is a "toolkit and runtime for building highly concurrent, distributed, and fault tolerant event-driven applications on .NET". [https://getakka.net/]

Main concepts:
* https://getakka.net/articles/intro/what-is-akka.html
* https://getakka.net/articles/remoting/index.html

This solution contains three projects:
* AkkaDevices - Domain definition and Akka actors definition.
    * DeviceActor - Akka actor representation of a device that sends measurements every second.
    * DeviceManagerActor - Akka actor representation of a device manager that computes the number of received measurements.
* AkkaDevices.Server - Server configuration and initialization.
* AkkaDevices.Client - Client configuration and initialization.

## Build

`dotnet build`

## Run

#### Manually

Server:

`dotnet run -p AkkaDevices.Server`

Client:

`dotnet run -p AkkaDevices.Client <CLIENT_ID> <DEVICE_NUMBER>`

* `<CLIENT_ID>` - Client identifier, shown in the console window when a device measurement is sent or received.
* `<DEVICE_NUMBER>` - Number of devices created by this client. Each device sends one measurement per second to the server.

Example:

`dotnet run -p AkkaDevices.Client 1 100`

#### run.bat (Windows)

Run:

`run.bat`

This batch file builds the solution and automatically starts the server and three clients, each client creates 100 devices:

```
dotnet build
start "Server" dotnet run --no-build -p AkkaDevices.Server
start "Client 1" dotnet run --no-build -p AkkaDevices.Client 1 100
start "Client 2" dotnet run --no-build -p AkkaDevices.Client 2 100
start "Client 3" dotnet run --no-build -p AkkaDevices.Client 3 100
```

## Output

Client output:

`<CLIENT_NAME> - <RANDOM_MEASUREMENT>`

Example:

`akka://device-manager-client-2/user/74c8e91f-e457-45c4-b269-c320e4cea2a6 - 347855557`

Server output:

`<TOTAL_MESSAGE_COUNT> - <CLIENT_NAME> - <RANDOM_MEASUREMENT>`

Example:

`1 - akka://device-manager-client-2/user/339fd6ab-22d6-4ead-bdee-44ef90d721ff - 1770950464`
