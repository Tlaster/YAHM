using System;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.CPU;

namespace Tlaster.YAHM.Models;

public record CpuModel(
    string Name,
    string Vendor,
    string Architecture,
    string Speed,
    string Cores,
    string Threads,
    string Temperature)
{
    public static readonly CpuModel Empty = new CpuModel("", "", "", "", "", "", "");

    public static CpuModel FromGenericCpu(GenericCpu cpu)
    {
        var temp = cpu.Sensors.Where(s => s.SensorType == SensorType.Temperature);
        var temperature = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature)?.Value?.ToString() ?? string.Empty;
        return new CpuModel(cpu.Name, "", "", "", "", "", temperature);
    }
}