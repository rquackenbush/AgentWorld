using Avalonia.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AgentWorld.ViewModels
{
    public class SafeObservableCollection<T> : ObservableCollection<T>
    {
        public SafeObservableCollection()
        {
        }

        public SafeObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public SafeObservableCollection(List<T> list) : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.UIThread.Post(() => base.OnCollectionChanged(e));
            
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            Dispatcher.UIThread.Post(() => base.OnPropertyChanged(e));
        }
    }
}
