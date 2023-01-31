using Swole_Patrol.Models;
using Swole_Patrol.Services;
using Swole_Patrol.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class LogCalViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private string date;
        private double calories;
        private string itemId;
        private double curCal;
        private ObservableCollection<Calories_Item> calories_array;
        private ObservableCollection<Calories_Item> calories_rev;
        public string Id { get; set; }
        
        public Command LogCommand { get; }
        public Command CancelCommand { get; }

        public LogCalViewModel()
        {
            LogCommand = new Command(OnLog, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => LogCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(date)
                && !String.IsNullOrWhiteSpace(calories.ToString());
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public double Calories
        {
            get => calories;
            set => SetProperty(ref calories, value);
        }

        public double CurCal
        {
            get => curCal;
            set=> SetProperty(ref curCal, value);
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
        public ObservableCollection<Calories_Item> Calories_Array
        {
            get => calories_array;
            set
            {
                SetProperty(ref calories_array, value);               
            }
        }
        public ObservableCollection<Calories_Item> Calories_Rev
        {
            get => calories_rev;
            set
            {
                SetProperty(ref calories_rev, value);              
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await repository.GetItem(itemId);
                Id = item.Id;
                Calories_Array = item.Calories_Array;
                Calories_Rev = Calories_Array;
                Calories_Rev = new ObservableCollection<Calories_Item>(Calories_Rev.Reverse());
                Date = DateTime.Now.ToString();
                CountCal();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnLog()
        {
            Calories_Item calorieEntry = new Calories_Item(Date, Calories);
            Item item  = await repository.GetItem(ItemId);
            
            if (item.Calories_Array.Count == 1 && item.Calories_Array[0].Dt == null)
            {
                item.Calories_Array = new ObservableCollection<Calories_Item>();
            }

            item.Calories_Array.Add(calorieEntry);
            await repository.UpdateItem(item);
            LoadItemId(ItemId);
            Date= DateTime.Now.ToString();
            Calories = 0;
        }

        private async void CountCal()
        {
            Item item = await repository.GetItem(ItemId);
            CurCal = 0;
            DateTime parsedDate; 
            foreach(var x in item.Calories_Array)
            {
                parsedDate = Convert.ToDateTime(x.Dt);
                if(parsedDate >= DateTime.Today)
                {
                    CurCal += x.Value;
                }
            }
        }
    }
}
