using System.Collections.Generic; 
namespace ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.MealByIdDTOs{ 

    public class WinePairing
    {
        public List<object> pairedWines { get; set; }
        public string pairingText { get; set; }
        public List<object> productMatches { get; set; }
    }

}