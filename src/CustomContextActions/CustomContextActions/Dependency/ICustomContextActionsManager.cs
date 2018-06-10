using System;
using Xamarin.Forms;

namespace CustomContextActions.Dependency
{
    public interface ICustomContextActionsManager
    {
        void RestoreContextActionsViews();
        void SetCustomBackgroundColor(Xamarin.Forms.Color color, bool isForDestructive = false);
        void SetCustomView(Xamarin.Forms.View color, bool isForDestructive = false);
    }
}
