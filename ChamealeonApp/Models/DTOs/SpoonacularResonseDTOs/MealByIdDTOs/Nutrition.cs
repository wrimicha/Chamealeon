using System.Collections.Generic; 
namespace ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.MealByIdDTOs{ 

    public class Nutrition
    {
        public List<Nutrient> nutrients { get; set; }
        public List<Property> properties { get; set; }
        public List<Flavonoid> flavonoids { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public CaloricBreakdown caloricBreakdown { get; set; }
        public WeightPerServing weightPerServing { get; set; }
    }

}