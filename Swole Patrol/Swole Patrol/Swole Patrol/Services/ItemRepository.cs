using Firebase.Database;
using Newtonsoft.Json;
using Swole_Patrol.Models;
using System;
using System.Collections.Generic;
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
    }
}
