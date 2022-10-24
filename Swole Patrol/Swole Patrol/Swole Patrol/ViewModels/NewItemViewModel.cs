using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private string username;
        private string password;
        private string name;
        private string birthday;
        private string gender;
        private int height;
        private double weight;
        private string email;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(username)
                && !String.IsNullOrWhiteSpace(password)
                && !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(birthday.ToString())
                && !String.IsNullOrWhiteSpace(gender)
                && !String.IsNullOrWhiteSpace(height.ToString())
                && !String.IsNullOrWhiteSpace(weight.ToString())
                && !String.IsNullOrWhiteSpace(email);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Birthday
        {
            get => birthday;
            set => SetProperty(ref birthday, value);
        }

        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        public int Height 
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public double Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Username = Username,
                Password = Password,
                Name = Name,
                Birthday = DateTime.Parse(Birthday).Date,
                Gender = Gender.ToLower(),
                Height = Height,
                Weight = Weight,
                Email = Email.ToLower()
            };

            //await DataStore.AddItemAsync(newItem);
            var isSaved = await repository.Save(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
