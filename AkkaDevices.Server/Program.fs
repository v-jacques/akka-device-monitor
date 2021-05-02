open Akka.Actor
open Akka.Configuration
open Akka.FSharp
open Actors
open System

[<EntryPoint>]
let main argv =
    Console.ForegroundColor <- ConsoleColor.Green // Green console color to differentiate server from clients.
    
    let serverConfig = ConfigurationFactory.ParseString """
        akka.actor {
            provider = remote
        }
        akka.remote.dot-netty.tcp {
            port = 8081
            hostname = localhost
        }"""

    let system = System.create "device-manager-server" serverConfig // Akka actor system.
    system.ActorOf(Props(typedefof<DeviceManagerActor>, [||]), "device-manager") |> ignore // Device manager creation.

    Console.ReadKey() |> ignore // Runs application until a key is pressed.
    system.Terminate() |> Async.AwaitTask |> Async.RunSynchronously

    0 // Return an integer exit code.
