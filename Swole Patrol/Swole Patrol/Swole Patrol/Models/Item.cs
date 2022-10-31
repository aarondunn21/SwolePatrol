using System;
using System.Collections.ObjectModel;

namespace Swole_Patrol.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public string Email { get; set; }
        public ObservableCollection<Calories_Item> Calories_Array { get; set; }
    }
}