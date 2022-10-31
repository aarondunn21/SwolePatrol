namespace Swole_Patrol.Models
{
    public class Calories_Item
    {
        public string Dt { get; set; }

        public double Value { get; set; }

        public Calories_Item(string dt, double value)
        {
            Dt = dt;
            Value = value;
        }
    }
}
