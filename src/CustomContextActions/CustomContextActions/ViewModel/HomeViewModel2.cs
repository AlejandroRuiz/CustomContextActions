using System;
using System.Collections.ObjectModel;
using CustomContextActions.Dependency;
using MvvmHelpers;
using Xamarin.Forms;

namespace CustomContextActions.ViewModel
{
    public class HomeViewModel2 : BaseViewModel
    {
        readonly ICustomContextActionsManager _manager;

        public HomeViewModel2()
        {
            _manager = DependencyService.Get<ICustomContextActionsManager>();
            _manager.SetCustomBackgroundColor(Color.Orange, true);
            _manager.SetCustomBackgroundColor(Color.Accent);
        }

        public ObservableCollection<string> Fruits { get; } = new ObservableCollection<string>
        {
            "Orange",
            "Banana",
            "Watermelon"
        };
    }
}
