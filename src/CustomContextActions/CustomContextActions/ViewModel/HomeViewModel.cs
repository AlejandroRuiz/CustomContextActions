using System;
using MvvmHelpers;
using System.Collections.ObjectModel;
using CustomContextActions.Dependency;
using Xamarin.Forms;
using CustomContextActions.Controls;
namespace CustomContextActions.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        readonly ICustomContextActionsManager _manager;

        public HomeViewModel()
        {
            _manager = DependencyService.Get<ICustomContextActionsManager>();
            _manager.SetCustomView(new DeleteContextActionView(), true);
            _manager.SetCustomView(new SaveContextActionView());
        }

        public ObservableCollection<string> Fruits { get; } = new ObservableCollection<string>
        {
            "Orange",
            "Banana",
            "Watermelon"
        };
    }
}
