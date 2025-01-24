using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace AgentWorld.ViewModels;

public class ViewModelBase : ObservableObject
{
    //protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    //{
    //    Dispatcher.UIThread.Post(() => base.OnPropertyChanged(e));
    //}

    //protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    //{
    //    Dispatcher.UIThread.Post(() => base.OnPropertyChanging(e));
    //}
}
