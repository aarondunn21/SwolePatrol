namespace Swole_Patrol.Models
{
    public class Workout_Item
    {
        public string Exercise { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public Workout_Item(string exercise, int sets, int reps)
        {
            Exercise = exercise;
            Sets = sets;
            Reps = reps;
        }
    }
}
