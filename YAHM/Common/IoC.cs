using Jab;
using Tlaster.YAHM.Repository;

namespace Tlaster.YAHM.Common;

[ServiceProvider]
[Singleton(typeof(IHardwareRepository), typeof(HardwareRepository))]
internal partial class IoC
{
    public static IoC Default { get; } = new();
}