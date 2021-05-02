module Actors

open Akka.Actor
open Akka.FSharp
open Domain
open System

/// Each device sends measurement messages to the device manager.
type DeviceActor (manager: ActorSelection) =
    inherit UntypedActor()

    let deviceName = DeviceActor.Context.Self.Path.ToStringWithAddress()
    let rng = Random() 

    override _.OnReceive (_: obj) =
        let deviceMeasurement = rng.Next()
        manager <! DeviceMessage(deviceName, deviceMeasurement) // Sends measurement to device manager.
        printfn "%s - %i" deviceName deviceMeasurement

/// The device manager receives measurements from devices and computes the number of received messages.
type DeviceManagerActor () =
    inherit UntypedActor()

    let mutable messageCount = 0 // Computes the number of messages received from devices.

    override _.OnReceive (message: obj) =
        match message with
        | :? DeviceMessage as deviceMessage ->
            let deviceName, deviceMeasurement = deviceMessage
            messageCount <- messageCount + 1
            printfn "%i - %s - %i" messageCount deviceName deviceMeasurement
        | _ -> ()