dotnet build
start "Server" dotnet run --no-build -p AkkaDevices.Server
start "Client 1" dotnet run --no-build -p AkkaDevices.Client 1 100
start "Client 2" dotnet run --no-build -p AkkaDevices.Client 2 100
start "Client 3" dotnet run --no-build -p AkkaDevices.Client 3 100
