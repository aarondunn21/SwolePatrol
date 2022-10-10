using Firebase.Database;
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
    }
}
