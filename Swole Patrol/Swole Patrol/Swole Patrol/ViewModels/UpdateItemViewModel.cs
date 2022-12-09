using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class UpdateItemViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();
        private string itemId;
        private string username;
        private string password;
        private string name;
        private string birthday;
        private string gender;
        private int height;
        private double weight;
        private string email;
        private ObservableCollection<Calories_Item> caloriesArray;
        private ObservableCollection<Weight_Item> weightArray;

        public string Id { get; set; }

        public UpdateItemViewModel()
        {
            UpdateCommand = new Command(OnUpdate, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(username)
                && !String.IsNullOrWhiteSpace(password)
                && !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(birthday)
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
        public ObservableCollection<Calories_Item> Calories_Array
        {
            get => caloriesArray;
            set => SetProperty(ref caloriesArray, value);
        }

        public ObservableCollection<Weight_Item> Weight_Array
        {
            get => weightArray;
            set => SetProperty(ref weightArray, value);

        }

        public Command UpdateCommand { get; }
        public Command CancelCommand { get; }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await repository.GetItem(itemId);
                Id = item.Id;
                Username = item.Username;
                Password = item.Password;
                Name = item.Name;
                Birthday = item.Birthday.ToString();
                Gender = item.Gender;
                Height = item.Height;
                Weight = item.Weight;
                Email = item.Email;
                Calories_Array = item.Calories_Array;
                Weight_Array = item.Weight_Array;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnUpdate()
        {
            Item newItem = new Item()
            {
                Id = ItemId,
                Username = Username,
                Password = Password,
                Name = Name,
                Birthday = DateTime.Parse(Birthday).Date,
                Gender = Gender.ToLower(),
                Height = Height,
                Weight = Weight,
                Email = Email.ToLower(),
                Calories_Array = Calories_Array,
                Weight_Array = Weight_Array
            };

            //await DataStore.AddItemAsync(newItem);
            await repository.UpdateItem(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
