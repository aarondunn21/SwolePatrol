
using System.Windows.Input;
using Swole_Patrol.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command AddItemCommand { get; }
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            LoginCommand = new Command(OnLogin);
            AddItemCommand = new Command(OnAddItem);

        }

        public ICommand OpenWebCommand { get; }
        private async void OnLogin(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
    }
}