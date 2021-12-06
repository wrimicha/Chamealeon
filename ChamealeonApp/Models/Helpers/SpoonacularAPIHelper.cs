using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ChamealeonApp.Models.Entities;

namespace ChamealeonApp.Models.Helpers
{


    

    public static class SpoonacularAPIHelper
    {
        //mike



//amir
        //HELPER FUNC: GET full details of selected meal and save to db as a proper Meal object





        //GET full meal plan for the week request

        static HttpClient client = new HttpClient();

        public static async Task<String> GenerateMealPlanFromSpoonacularAsync(string diet, int cals = 2000)
        {

            //GET https://api.spoonacular.com/mealplanner/generate

            //https://spoonacular.com/food-api/docs#Generate-Meal-Plan


            var queryString = $"https://api.spoonacular.com/mealplanner/generate?timeFrame=week&diet={diet}&targetCalories={cals}&apiKey=eaa80ce8fa5c4a2fa1a3c6c875ef9bf5";

            var jsonResponse = "";

            HttpResponseMessage response = await client.GetAsync(queryString);
            if (response.IsSuccessStatusCode)
            {
                jsonResponse = await response.Content.ReadAsStringAsync();
            }

            Console.WriteLine(jsonResponse);

            return jsonResponse;

        }
    }
}