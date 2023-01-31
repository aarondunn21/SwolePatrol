using Swole_Patrol.Models;
using Swole_Patrol.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Swole_Patrol.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class LogWorkoutViewModel : BaseViewModel
    {

        ItemRepository repository = new ItemRepository();

        private string itemId;
        private string exercise;
        private int sets;
        private int reps;
        private ObservableCollection<Workout_Item> workout_array;
        public string Id { get; set; }

        public Command LogCommand { get; }
        public Command CancelCommand { get; }

        public LogWorkoutViewModel()
        {
            LogCommand = new Command(OnLog, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => LogCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(exercise)
                && !String.IsNullOrWhiteSpace(reps.ToString())
                && !String.IsNullOrWhiteSpace(sets.ToString());
        }

        public string Exercise
        {
            get => exercise;
            set => SetProperty(ref exercise, value);
        }

        public int Sets
        {
            get => sets;
            set => SetProperty(ref sets, value);
        }

        public int Reps
        {
            get => reps;
            set => SetProperty(ref reps, value);
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
        public ObservableCollection<Workout_Item> Workout_Array
        {
            get => workout_array;
            set => SetProperty(ref workout_array, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await repository.GetItem(itemId);
                Id = item.Id;
                Workout_Array = item.Workout_Array;

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
            Workout_Item workoutEntry = new Workout_Item(Exercise, Sets, Reps);
            Item item = await repository.GetItem(ItemId);

            if (item.Workout_Array.Count == 1 && item.Workout_Array[0].Exercise == null)
            {
                item.Workout_Array = new ObservableCollection<Workout_Item>();
            }

            item.Workout_Array.Add(workoutEntry);
            await repository.UpdateItem(item);

            LoadItemId(item.Id);
        }
    }
}
