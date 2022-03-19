using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Tlaster.YAHM.Lifecycle.Controls;

namespace Tlaster.YAHM.Views;

public partial class MonitorView : Activity
{
    public MonitorView()
    {
        InitializeComponent();
    }

    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}