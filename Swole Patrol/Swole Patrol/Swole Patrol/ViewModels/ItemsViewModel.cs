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
    public class ItemsViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private Item _selectedItem;
        private ObservableCollection<Calories_Item> calories_array;
        public Item Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command DeleteItemCommand { get; }
        public Command UpdateItemCommand { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new Item();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DeleteItemCommand = new Command<Item>(OnItemDeleted);

            UpdateItemCommand = new Command<Item>(OnUpdateItem);
        }
        public ObservableCollection<Calories_Item> Calories_Array
        {
            get => calories_array;
            set => SetProperty(ref calories_array, value);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Debug.WriteLine("Loading Item");
                Items = await repository.GetItem("sgsg");   ///add itemId arg      
                Calories_Array = Items.Calories_Array;
                    
                //    new ObservableCollection<Calories_Item>
                //{
                //    new Calories_Item("Sun", 21),
                //    new Calories_Item("Mon", 24),
                //    new Calories_Item("Tue", 36),
                //    new Calories_Item("Wed", 38),
                //    new Calories_Item("Thu", 54),
                //    new Calories_Item("Fri", 57),
                //    new Calories_Item("Sat", 70)
                //};
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
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
    }
}