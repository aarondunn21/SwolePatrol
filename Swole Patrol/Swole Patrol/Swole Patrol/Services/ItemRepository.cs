using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Swole_Patrol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<Item>> GetAll()
        {
            return (await firebaseClient.Child(nameof(Item)).OnceAsync<Item>()).Select(item => new Item
            {
                Id = item.Object.Id,
                Text = item.Object.Text,
                Description = item.Object.Description
            }).ToList();
        }

        public async Task<Item> GetItem(string itemId)
        {
            var allItems = await GetAll();
            return allItems.Where(a => a.Id == itemId).FirstOrDefault();
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