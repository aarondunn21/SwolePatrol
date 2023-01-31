using Swole_Patrol.Views;
using System;
using Xamarin.Forms;

namespace Swole_Patrol
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(HealthPage), typeof(HealthPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(UpdateItemPage), typeof(UpdateItemPage));
            Routing.RegisterRoute(nameof(LogCalPage), typeof(LogCalPage));
            Routing.RegisterRoute(nameof(LogWeightPage), typeof(LogWeightPage));
            Routing.RegisterRoute(nameof(LogWorkoutPage), typeof(LogWorkoutPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
