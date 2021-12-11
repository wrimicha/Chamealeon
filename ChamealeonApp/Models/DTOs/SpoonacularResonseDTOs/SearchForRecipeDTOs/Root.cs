using System.Collections.Generic; 
namespace ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.SearchForRecipeDTOs{ 

    public class Root
    {
        public List<Result> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }

}