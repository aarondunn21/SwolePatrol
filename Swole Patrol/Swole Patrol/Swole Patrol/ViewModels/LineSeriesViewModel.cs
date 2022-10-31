using Swole_Patrol.Models;
using System.Collections.ObjectModel;


namespace Swole_Patrol.ViewModels
{
    public class LineSeriesViewModel
    {

        public ObservableCollection<Calories_Item> LineData1 { get; set; }

        public ObservableCollection<Calories_Item> LineData2 { get; set; }


        public LineSeriesViewModel()
        {
            LineData1 = new ObservableCollection<Calories_Item>
            {
                new Calories_Item("Sun", 21),
                new Calories_Item("Mon", 24),
                new Calories_Item("Tue", 36),
                new Calories_Item("Wed", 38),
                new Calories_Item("Thu", 54),
                new Calories_Item("Fri", 57),
                new Calories_Item("Sat", 70)
            };

            LineData2 = new ObservableCollection<Calories_Item>
            {
                new Calories_Item("Sun", 28),
                new Calories_Item("Mon", 44),
                new Calories_Item("Tue", 48),
                new Calories_Item("Wed", 50),
                new Calories_Item("Thu", 66),
                new Calories_Item("Fri", 78),
                new Calories_Item("Sat", 84)
            };
        }
    }
}
