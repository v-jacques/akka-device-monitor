module Domain

/// Each device has a unique name.
type DeviceName = string

/// Each device produces measurements.
type DeviceMeasurement = int

/// Each device sends messages to the device manager.
type DeviceMessage = DeviceName * DeviceMeasurement
