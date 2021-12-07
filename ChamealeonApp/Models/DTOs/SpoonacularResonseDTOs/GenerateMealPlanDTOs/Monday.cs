using System.Collections.Generic; 
namespace ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs{ 

    public class Monday
    {
        public List<Meal> meals { get; set; }
        public Nutrients nutrients { get; set; }
    }

}