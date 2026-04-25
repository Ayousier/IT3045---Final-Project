namespace IT3045___Final_Project.Models
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cuisine { get; set; } = string.Empty;
        public int Calories { get; set; }
        public string MealType { get; set; } = string.Empty; // e.g., Breakfast, Lunch, Dinner, Snack
    }
}
