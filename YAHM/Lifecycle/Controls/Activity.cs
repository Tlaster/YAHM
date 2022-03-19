using System;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace Tlaster.YAHM.Lifecycle.Controls;

public class Activity: UserControl
{
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);
        if (DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}