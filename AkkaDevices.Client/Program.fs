open Akka.Actor
open Akka.Configuration
open Akka.FSharp
open Actors
open System

[<EntryPoint>]
let main argv =
    let clientConfig = ConfigurationFactory.ParseString """
        akka.actor {
            provider = remote
        }
        akka.remote.dot-netty.tcp {
            port = 0 # bound to a dynamic port assigned by the OS
            hostname = localhost
        }"""

    let system = System.create "device-manager-client" clientConfig // Akka actor system.
    let managerPath = system.ActorSelection "akka.tcp://device-manager-server@localhost:8081/user/device-manager"
    
    for _ in 1..500 do
        let globalIdentifier = Guid.NewGuid().ToString() // Unique name for each device.
        let device = system.ActorOf(Props(typedefof<DeviceActor>, [|box managerPath|]), globalIdentifier) // Device creation.
        system.Scheduler.ScheduleTellRepeatedly(0, 1000, device, true, ActorRefs.NoSender) // Device measurement scheduling: 1 message per second per client.

    while true do () // Infinite loop to keep process alive.

    0 // Return an integer exit code.
