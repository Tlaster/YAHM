using System;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Tlaster.YAHM.Common;
using Tlaster.YAHM.Lifecycle.ViewModel;
using Tlaster.YAHM.Models;
using Tlaster.YAHM.Repository;

namespace Tlaster.YAHM.ViewModels;

public sealed partial class MonitorViewModel: ViewModel
{
    private readonly IHardwareRepository _hardwareRepository = IoC.Default.GetService<IHardwareRepository>();
    [ObservableProperty] private CpuModel _cpu = CpuModel.Empty;
    [ObservableProperty] private GpuModel _gpu = GpuModel.Empty;

    public MonitorViewModel()
    {
        _hardwareRepository.Cpu.SubscribeIn(this, cpu => Cpu = cpu);
        _hardwareRepository.Gpu.SubscribeIn(this, gpu => Gpu = gpu);
    }
}