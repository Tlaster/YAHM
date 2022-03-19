using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Tlaster.YAHM.Lifecycle.ViewModel;

public sealed class ViewModelScope: IDisposable
{
    private readonly List<IDisposable> _disposables = new();
    
    internal void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

    public void Dispose()
    {
        _disposables.ForEach(x => x.Dispose());
    }
}

public static class RxExtensions
{
    public static void SubscribeIn<T>(this IObservable<T> source, ViewModel viewModel, Action<T> onNext)
    {
        viewModel.Scope.Add(source.Subscribe(onNext));
    }
}