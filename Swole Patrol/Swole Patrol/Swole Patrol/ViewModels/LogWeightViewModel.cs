using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class LogWeightViewModel : BaseViewModel
    {
        ItemRepository repository = new ItemRepository();

        private string date;
        private double bmi;
        private double weight;
        private string itemId;
        private double height;
        private double curweight;
        private string bmiRange;
        private ObservableCollection<Weight_Item> weight_array;
        private ObservableCollection<Weight_Item> weight_rev;
        public string Id { get; set; }

        public Command LogCommand { get; }
        public Command CancelCommand { get; }

        public LogWeightViewModel()
        {
            LogCommand = new Command(OnLog, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => LogCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(date)
                && !String.IsNullOrWhiteSpace(weight.ToString());
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public double BMI
        {
            get => bmi;
            set => SetProperty(ref bmi, value);
        }

        public double Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        public double Height
        {
            get => height;
            set=> SetProperty(ref height, value);
        }

        public double CurWeight
        {
            get => curweight;
            set => SetProperty(ref curweight, value);
        }

        public string BmiRange
        {
            get => bmiRange;
            set => SetProperty(ref bmiRange, value);

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
        public ObservableCollection<Weight_Item> Weight_Array
        {
            get => weight_array;
            set => SetProperty(ref weight_array, value);
        }
        public ObservableCollection<Weight_Item> Weight_Rev
        {
            get => weight_rev;
            set => SetProperty(ref weight_rev, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await repository.GetItem(itemId);
                CurWeight = item.Weight;
                Id = item.Id;
                Weight_Array = item.Weight_Array;
                Weight_Rev = Weight_Array;
                Weight_Rev = new ObservableCollection<Weight_Item>(Weight_Rev.Reverse());
                Date = DateTime.Now.ToString();
                BMI = 703 * (item.Weight / (item.Height * item.Height));
                BMI = Math.Round(BMI, 2);
                ComputeBmiRange(BMI);
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
            Weight_Item weightEntry = new Weight_Item(Date, Weight);
            Item item = await repository.GetItem(ItemId);

            if (item.Weight_Array.Count == 1 && item.Weight_Array[0].Dt == null)
            {
                item.Weight_Array = new ObservableCollection<Weight_Item>();
            }

            item.Weight_Array.Add(weightEntry);
            item.Weight = Weight;
            await repository.UpdateItem(item);
            LoadItemId(itemId);
        }

        private void ComputeBmiRange(double bmi)
        {
            if(bmi >= 0 && bmi < 18.5)
            {
                BmiRange = "(Underwight)";
            }
            else if(bmi >= 18.5 && bmi < 25)
            {
                BmiRange = "(Healthy Weight)";
            }
            else if(bmi >= 25 && bmi < 30)
            {
                BmiRange = "(Overweight)";
            }
            else if (bmi >= 30)
            {
                BmiRange = "(Obese)";
            }
            else
            {
                BmiRange = "Invalid BMI";
            }
        }
    }
}
