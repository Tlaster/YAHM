﻿using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Tlaster.YAHM.Lifecycle.ViewModel;

[ObservableObject]
public abstract partial class ViewModel : IDisposable
{
    protected internal ViewModelScope Scope { get; } = new();
    public void Dispose()
    {
        Scope.Dispose();
    }
}