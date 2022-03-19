using System;
using System.Linq;
using LibreHardwareMonitor.Hardware;

namespace Tlaster.YAHM.Models;

public record GpuModel(
    string Name,
    string Vendor,
    float Speed,
    float Power,
    float Temperature,
    float Load)
{
    public static GpuModel Empty { get; } = new GpuModel("", "", 0, 0, 0, 0);
    public static GpuModel FromHardware(IHardware hardware)
    {
        var name = hardware.Name ?? string.Empty;
        var vendor = hardware.HardwareType switch
        {
            HardwareType.GpuNvidia => "Nvidia",
            HardwareType.GpuAmd => "AMD",
            HardwareType.GpuIntel => "Intel",
            _ => throw new ArgumentOutOfRangeException()
        };
        var speed = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock)?.Value ?? 0;
        var power = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power)?.Value ?? 0;
        var temperature = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature)?.Value ?? 0;
        var load = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load)?.Value ?? 0;
        return new GpuModel(name, vendor, speed, power, temperature, load);
    }
}