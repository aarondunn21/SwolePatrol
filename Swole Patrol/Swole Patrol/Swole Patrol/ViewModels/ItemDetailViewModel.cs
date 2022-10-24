using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
