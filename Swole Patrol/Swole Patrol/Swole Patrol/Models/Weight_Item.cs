using System;

namespace Swole_Patrol.Models
{
    public class Weight_Item
    {
        public string Dt { get; set; }

        public double Value { get; set; }

        public Weight_Item(string dt, double value)
        {
            Dt = dt;
            Value = value;
        }
    }
}
