namespace ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.MealByIdDTOs{ 

    public class Nutrient
    {
        public string name { get; set; }
        public string title { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public double percentOfDailyNeeds { get; set; }
    }

}