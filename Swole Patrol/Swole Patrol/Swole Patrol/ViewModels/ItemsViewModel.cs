using Swole_Patrol.Models;
using Swole_Patrol.Services;
using Swole_Patrol.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemsViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private Item _selectedItem;
        

        public ObservableCollection<Item> Items { get; }
        //public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        
        public Command<Item> ItemTapped { get; }
        public Command DeleteItemCommand { get; }
        public Command UpdateItemCommand { get; }
        public Command LogCaloriesCommand { get; }
        public Command LogWeightCommand { get; }
        public Command LogWorkoutCommand { get;  }

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
                Items.Clear();
                Items.Add(item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public ItemsViewModel()
        {
            //Title = "Browse";
            Items = new ObservableCollection<Item>();
            
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command<Item>(OnItemDeleted);
            UpdateItemCommand = new Command<Item>(OnUpdateItem);
            LogCaloriesCommand = new Command<Item>(OnLogCalories);
            LogWeightCommand = new Command<Item>(OnLogWeight);
            LogWorkoutCommand = new Command<Item>(OnLogWorkout);


        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnItemDeleted(Item item)
        {
            await repository.DeleteItem(item.Id);
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
        async void OnUpdateItem(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(UpdateItemPage)}?{nameof(UpdateItemViewModel.ItemId)}={item.Id}");
            return;
        }
        async void OnLogCalories(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LogCalPage)}?{nameof(LogCalViewModel.ItemId)}={item.Id}");
            return;
        }
        async void OnLogWeight(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LogWeightPage)}?{nameof(LogWeightViewModel.ItemId)}={item.Id}");
            return;
        }

        async void OnLogWorkout(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LogWorkoutPage)}?{nameof(LogWorkoutViewModel.ItemId)}={item.Id}");
            return;
        }

    }
}