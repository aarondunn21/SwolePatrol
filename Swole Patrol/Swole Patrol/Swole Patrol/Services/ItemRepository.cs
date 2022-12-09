using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Swole_Patrol.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Swole_Patrol.Services
{
    internal class ItemRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://swole-patrol-b7088-default-rtdb.firebaseio.com/");

        public async Task<bool> Save(Item item)
        {
            var data = await firebaseClient.Child(nameof(Item)).PostAsync(JsonConvert.SerializeObject(item));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        //public async Task<Item> GetAll()
        //{
            
        //}

        public async Task<Item> GetItem(string itemId)
        {
            var data = await firebaseClient.Child(nameof(Item)).OnceAsync<Item>();

            Item usr = new Item();
            foreach (var item in data)
            {
                if (item.Object.Id == "f99c4afc-99eb-4cc8-b06f-1a71a17a5946") //add itemID arg
                {
                    usr.Id = item.Object.Id;
                    usr.Username = item.Object.Username;
                    usr.Password = item.Object.Password;
                    usr.Name = item.Object.Name;
                    usr.Birthday = item.Object.Birthday;
                    usr.Gender = item.Object.Gender; //
                    usr.Height = item.Object.Height;
                    usr.Weight = item.Object.Weight;
                    usr.Email = item.Object.Email;
                    usr.Calories_Array = new ObservableCollection<Calories_Item>();
                    foreach (var cal in item.Object.Calories_Array)
                    {
                        usr.Calories_Array.Add(new Calories_Item(cal.Dt, cal.Value));
                    }
                }
            }

            return usr;
        }

        public async Task DeleteItem(string itemId)
        {
            {
                var toDeleteItem = (await firebaseClient.Child("Item").OnceAsync<Item>())
                    .Where(item => item.Object.Id == itemId).FirstOrDefault();
                await firebaseClient.Child("Item").Child(toDeleteItem.Key).DeleteAsync();
                return;
            }
        }
    }
}