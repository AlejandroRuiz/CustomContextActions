using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomContextActions.View;

namespace CustomContextActions
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void Handle_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        public void Handle_Clicked_CustomView(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage2());
        }
    }
}
