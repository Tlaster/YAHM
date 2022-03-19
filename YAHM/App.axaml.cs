using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using FluentAvalonia.UI.Controls;

namespace Tlaster.YAHM;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime:
                classicDesktopStyleApplicationLifetime.MainWindow = new CoreWindow
                {
                    Content = new RootShell(),
                    Title = "Yet Another Hardware Monitor",
                    TransparencyLevelHint = WindowTransparencyLevel.Mica,
                    Background = null,
                    ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.PreferSystemChrome,
                    ExtendClientAreaToDecorationsHint = true
                };
                break;
            case ISingleViewApplicationLifetime singleViewApplicationLifetime:
                singleViewApplicationLifetime.MainView = new RootShell();
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }
}