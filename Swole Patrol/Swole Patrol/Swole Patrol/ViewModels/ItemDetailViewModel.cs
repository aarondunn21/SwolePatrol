using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private string itemId;
        private string username;
        private string password;
        private string name;
        private DateTime birthday;
        private string gender;
        private int height;
        private double weight;
        private string email;
        private ObservableCollection<Calories_Item> calories_array;
        private ObservableCollection<Weight_Item> weight_array;
        private ObservableCollection<Workout_Item> workout_array;
        public string Id { get; set; }

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

        public DateTime Birthday
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
            get => calories_array;
            set => SetProperty(ref calories_array, value);
        }

        public ObservableCollection<Weight_Item> Weight_Array
        {
            get => weight_array;
            set => SetProperty(ref weight_array, value);
        }

        public ObservableCollection<Workout_Item> Workout_Array
        {
            get => workout_array;
            set => SetProperty(ref workout_array, value);
        }

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
                //var item = await DataStore.GetItemAsync(itemId);
                var item = await repository.GetItem(itemId);
                Id = item.Id;
                Username = item.Username;
                Password = item.Password;
                Name = item.Name;
                Birthday = item.Birthday;
                Gender = item.Gender;
                Height = item.Height;
                Weight = item.Weight;
                Email = item.Email;
                Calories_Array = item.Calories_Array;
                Weight_Array = item.Weight_Array;
                Workout_Array = item.Workout_Array;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
