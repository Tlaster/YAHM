using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.CPU;
using Tlaster.YAHM.Models;

namespace Tlaster.YAHM.Repository;

interface IHardwareRepository
{
    IObservable<CpuModel> Cpu { get; }
}

internal sealed class HardwareRepository: IHardwareRepository
{
    private readonly Computer _computer = new()
    {
        IsCpuEnabled = true,
        IsGpuEnabled = true,
        IsMemoryEnabled = true,
        IsMotherboardEnabled = true,
        IsControllerEnabled = true,
        IsNetworkEnabled = true,
        IsStorageEnabled = true,
        IsBatteryEnabled = true,
        IsPsuEnabled = true,
    };

    private readonly Subject<GenericCpu> _cpuRaw = new();
    public IObservable<CpuModel> Cpu => _cpuRaw.Select(CpuModel.FromGenericCpu);

    private Task _updateTask;

    public HardwareRepository()
    {
        _computer.Open();
        FindHardware(_computer.Hardware);
        _updateTask =  Task.Run(() => UpdateHardware(CancellationToken.None));
    }

    private async Task UpdateHardware(CancellationToken token)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), token);
            if (token.IsCancellationRequested)
            {
                break;
            }
            FindHardware(_computer.Hardware);
        }
    }
    
    private void FindHardware(IEnumerable<IHardware> hardware)
    {
        foreach (var item in hardware)
        {
            if (item is GenericCpu cpu)
            {
                cpu.Update();
                _cpuRaw.OnNext(cpu);
            }

            if (!item.SubHardware.Any())
            {
                continue;
            }

            FindHardware(item.SubHardware);
        }

    }

}