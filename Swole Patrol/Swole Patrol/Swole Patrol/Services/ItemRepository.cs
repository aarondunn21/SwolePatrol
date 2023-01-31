using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Swole_Patrol.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swole_Patrol.Services
{
    internal class ItemRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://swole-patrol-2-default-rtdb.firebaseio.com/");

        public async Task<bool> Save(Item item)
        {
            var data = await firebaseClient.Child(nameof(Item)).PostAsync(JsonConvert.SerializeObject(item));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<Item>> GetAll()
        {
            return (await firebaseClient.Child(nameof(Item)).OnceAsync<Item>()).Select(item => new Item
            {
                Id = item.Object.Id,
                Username = item.Object.Username,
                Password = item.Object.Password,
                Name = item.Object.Name,
                Birthday = item.Object.Birthday,
                Gender = item.Object.Gender,
                Height = item.Object.Height,
                Weight = item.Object.Weight,
                Email = item.Object.Email,
                Calories_Array = item.Object.Calories_Array,
                Weight_Array = item.Object.Weight_Array,
                Workout_Array = item.Object.Workout_Array
            }).ToList();
        }

        public async Task<Item> GetItem(string itemId)
        {
            var allItems = await GetAll();
            return allItems.Where(a => a.Id == itemId).FirstOrDefault();
        }

        public async Task<Item> GetItemWithUserName(string username) 
        {
            var allItems = await GetAll();
            return allItems.Where(a => a.Username == username).FirstOrDefault();
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
        public async Task UpdateItem(Item chosenItem)
        {
            {
                var toUpdateItem = (await firebaseClient.Child("Item").OnceAsync<Item>())
                    .Where(item => item.Object.Id == chosenItem.Id).FirstOrDefault();
                await firebaseClient.Child("Item").Child(toUpdateItem.Key).PutAsync(chosenItem);
                return;
            }
        }
    }
}