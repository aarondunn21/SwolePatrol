
using Swole_Patrol.Views;
using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command LoginCancel { get; }

        private string _usernameInput;
        private string _passwordInput;

        ItemRepository _repository = new ItemRepository();

        public string Username{
            get => _usernameInput;
            set => SetProperty(ref _usernameInput, value);
        }

        public string Password
        {
            get => _passwordInput;
            set => SetProperty(ref _passwordInput, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LoginCancel = new Command(OnLoginCancelClicked);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();

        }

        
        

        private async void OnLoginClicked()
        {
            try
            {
                var item = await _repository.GetItemWithUserName(Username);
                
                
                if(item == null || item.Password != this.Password)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    await Application.Current.MainPage.DisplayAlert("Login Status", "Login Unsuccesful please try again", "Close");

                }
                else if (item.Password == this.Password)
                {

                    await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}?{nameof(ItemsViewModel.ItemId)}={item.Id}");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async void OnLoginCancelClicked() 
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
