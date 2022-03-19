using System;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.CPU;

namespace Tlaster.YAHM.Models;

public record CpuModel(
    string Name,
    string Vendor,
    float Speed,
    int Cores,
    int Threads,
    float Power,
    float Temperature,
    float Load)
{
    public static readonly CpuModel Empty = new ("", "", 0f, 0, 0, 0, 0, 0);

    public static CpuModel FromGenericCpu(GenericCpu cpu)
    {
        var name = cpu.CpuId.FirstOrDefault()?.FirstOrDefault()?.BrandString ?? cpu.Name ?? string.Empty;
        var temperature = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature)?.Value ?? 0f;
        var vendor = cpu.CpuId.FirstOrDefault()?.FirstOrDefault()?.Vendor.ToString() ?? string.Empty;
        var core = cpu.Sensors.Count(it => it.SensorType == SensorType.Factor);
        var thread = cpu.CpuId.Length;
        var power = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Power)?.Value ?? 0f;
        var speed = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Clock)?.Value ?? 0f;
        var load = cpu.Sensors.ElementAtOrDefault(cpu.Sensors.Count(s => s.SensorType == SensorType.Load) - 1)?.Value ?? 0f;
        return new CpuModel(name, vendor, speed, core, thread, power, temperature, load);
    }
}