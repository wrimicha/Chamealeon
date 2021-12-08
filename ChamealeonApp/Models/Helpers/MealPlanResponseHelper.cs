using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs;
using ChamealeonApp.Models.Entities;

namespace ChamealeonApp.Models.Helpers
{
    public static class MealPlanResponseHelper
    {
        public static Models.Entities.Meal ConvertSpoonacularMealToFullMeal(int spoonacularId)
        {
            //create a meal object out of this meal and save to database
            var convertedMeal = new Entities.Meal();

            //1. get the FULL meal details from spoonacular using the mealplan meal id
            var spoonacularMeal = SpoonacularAPIHelper.GetFullDetailsOfMeal(spoonacularId);

            //2. convert the detailed meal into a full meal
            List<Ingredient> ingredients = new List<Ingredient>();
            // NutritionalInformation nutritionInfo = new NutritionalInformation();

            //use select to map the nutrition to ingredients
            //TODO: no cost avail and image is not the url, its a file name
            ingredients = (List<Ingredient>)spoonacularMeal.Result.extendedIngredients.Select(i => new Ingredient { Name = i.nameClean });
            convertedMeal.Ingredients = ingredients;
            //map the nutrional information
            double cals = 0;
            double carbs = 0;
            double fat = 0;
            double protein = 0;
            double sodium = 0;
            double sugar = 0;
            foreach (var nutrition in spoonacularMeal.Result.nutrition.nutrients)
            {

                //need carbohydrates, fat, protein, calories, sodium and sugar
                if (nutrition.name.Contains("Calories"))
                    cals = nutrition.amount; //kcal
                if (nutrition.name.Contains("Carbohydrates"))
                    carbs = nutrition.amount; //g
                if (nutrition.name.Contains("Sugar"))
                    sugar = nutrition.amount; //g
                if (nutrition.name.Contains("Sodium"))
                    sodium = nutrition.amount; //mg
                if (nutrition.name.Contains("Protein"))
                    protein = nutrition.amount;  //g
                if (nutrition.name.Contains("Fat"))
                    fat = nutrition.amount; //g


                var convertedNutrition = new NutritionalInformation
                {

                    Calories = cals,
                    Fat = fat,
                    Protein = protein,
                    Carbs = carbs,
                    Sodium = sodium,
                    Sugar = sugar

                };

                //add to nutrient property of the meal
                convertedMeal.NutritionInfo = convertedNutrition;
            }



            return convertedMeal;
        }

        //function to create a meal plan out of the root response
        public static MealPlan ConvertRootDTOToMealPlan(Root rootResponse)
        {
            //TODO: look into this for iterating through properties https://stackoverflow.com/questions/721441/c-sharp-how-to-iterate-through-classes-fields-and-set-properties
            MealPlan mealPlanObject = new MealPlan();

            //each mealplan has a list of days
            //sunday
            var sundayMeals = rootResponse.week.sunday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedSundayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in sundayMeals.meals)
            {
                //TODO: call convert rootomeal
                //1: call the full meal DTO and convert to a full meal
                //var convertedMeal = ConvertToFullMeal(spoonacularId);
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedSundayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Sunday, Meals = convertedSundayMeals });

            //monday
            var mondayMeals = rootResponse.week.monday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedMondayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in mondayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedMondayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Monday, Meals = convertedMondayMeals });




            //tuesday
            var tuesdayMeals = rootResponse.week.tuesday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedTuesdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in mondayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedTuesdayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Tuesday, Meals = convertedTuesdayMeals });




            //wednesday

            var wednesdayMeals = rootResponse.week.wednesday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedWednesdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in wednesdayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedWednesdayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Wednesday, Meals = convertedWednesdayMeals });






            //thursday

            var thursdayMeals = rootResponse.week.thursday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedThursdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in thursdayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedThursdayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Thursday, Meals = convertedThursdayMeals });





            //friday
            //monday
            var fridayMeals = rootResponse.week.friday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedFridayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in fridayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedFridayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Friday, Meals = convertedFridayMeals });







            //saturday
            //monday
            var saturdayMeals = rootResponse.week.saturday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedSaturdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in mondayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertSpoonacularMealToFullMeal(meal.id);

                //add to list of meals for sunday
                convertedSaturdayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Saturday, Meals = convertedSaturdayMeals });



            //last step, return full converted meal plan
            return mealPlanObject;
        }
    }
}