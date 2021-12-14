using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChamealeonApp.Models.DTOs;
using ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Persistence;

namespace ChamealeonApp.Models.Helpers
{
    //Author: Burhan
    //Purpose of the class is to take the response from Spoonacular API and convert it to the appopriate type for our app
    public static class MealPlanResponseHelper
    {

        //Author: Burhan
        //Takes the Spoonacular Id of a meal, gets the response from Spoonacular API of the FULL meal details and creates a proper Meal object
        public static async Task<Entities.Meal> ConvertSpoonacularMealToFullMealAsync(int spoonacularId, DataContext context)
        {
            // try
            // {
            //instantiate the converted meal object that will be persisted to db
            var convertedMeal = new Entities.Meal();

            //get the FULL meal details from spoonacular API using the mealplan spoonacular meal id
            var spoonacularMeal = await SpoonacularAPIHelper.GetFullDetailsOfMeal(spoonacularId);

            //check if the spoonacular meal exists already before adding to the db
            if (context.Meals.Any(m => m.SpoonacularMealId.Equals(spoonacularMeal.id)))
            {
                //meal already exists in db so dont need to add everything again, return the existing meal from the db
                return context.Meals.FirstOrDefault(m => m.SpoonacularMealId.Equals(spoonacularId));
            }

            //meal does not exist in our db so convert the detailed meal into a full meal

            //TODO: check if the ingredient exists before creating a new ingredient
            convertedMeal.Ingredients = spoonacularMeal.extendedIngredients.Select(i => new Ingredient { Name = i.nameClean }).ToList();

            //instaniate the nutrition information
            convertedMeal.NutritionInfo = new NutritionalInformation();
            //map the nutrional information
            foreach (var nutrition in spoonacularMeal.nutrition.nutrients)
            {
                //need carbohydrates, fat, protein, calories, sodium and sugar
                if (nutrition.name.Contains("Calories"))
                    convertedMeal.NutritionInfo.Calories = nutrition.amount; //kcal
                else if (nutrition.name.Contains("Carbohydrates"))
                    convertedMeal.NutritionInfo.Carbs = nutrition.amount; //g
                else if (nutrition.name.Contains("Sugar"))
                    convertedMeal.NutritionInfo.Sugar = nutrition.amount; //g
                else if (nutrition.name.Contains("Sodium"))
                    convertedMeal.NutritionInfo.Sodium = nutrition.amount; //mg
                else if (nutrition.name.Contains("Protein"))
                    convertedMeal.NutritionInfo.Protein = nutrition.amount;  //g
                else if (nutrition.name.Contains("Fat"))
                    convertedMeal.NutritionInfo.Fat = nutrition.amount; //g
            }

            //get image, cost, instructions, name, spoonacular id, prep time
            convertedMeal.ImageUrl = spoonacularMeal.image;
            convertedMeal.Cost = spoonacularMeal.pricePerServing;
            convertedMeal.Instructions = spoonacularMeal.instructions;
            convertedMeal.Name = spoonacularMeal.title;
            convertedMeal.SpoonacularMealId = spoonacularMeal.id;
            convertedMeal.PrepTime = spoonacularMeal.readyInMinutes;

            return convertedMeal;
            // }
            // catch (System.Exception)
            // {
            //     throw new Exception("An error has occured creating a Meal from the Spoonacular meal");
            // }
        }
        /*
        public static void AddMealToDay(List<Entities.Meal> convertedMeals, List<DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs.Meal> rootMeals)
        {
            //each mealplan has a list of days
            //sunday
            //var sundayMeals = rootResponse.week.sunday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            //List<Models.Entities.Meal> convertedSundayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in rootMeals.meals)
            {
                //TODO: call convert rootomeal
                //1: call the full meal DTO and convert to a full meal
                //var convertedMeal = ConvertToFullMeal(spoonacularId);
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                //check if the meal exists already before adding it to the mealplan
                // if (context.Meals.Find(convertedMeal.Id) != null)
                // {
                //     //meal already exists in db so dont need to add everything again
                //     continue;
                // }

                //add to list of meals for sunday
                convertedSundayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Sunday, Meals = convertedSundayMeals });
        }
        */

        //Author: Burhan
        //Uses the FULL meal plan response from Spoonacular API and creates a MealPlan object out of it, uses ConvertSpoonacularMealToFullMealAsync to get and create each meal
        public static async Task<MealPlan> ConvertRootDTOToMealPlanAsync(Root rootResponse, DataContext context)
        {
            // try
            // {
            //TODO: look into this for iterating through properties https://stackoverflow.com/questions/721441/c-sharp-how-to-iterate-through-classes-fields-and-set-properties
            /*
                        var days = rootResponse.week.GetType().GetProperties();
                        foreach (var day in days)
                        {
                            var meals = day.GetType().GetProperties();
                            Console.WriteLine(day.Name);
                            Console.WriteLine(day.GetConstantValue());
                            foreach (var meal in meals)
                            {
                                Console.WriteLine(meal.Name);
                                Console.WriteLine(meal.GetConstantValue());

                                DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs.Meal rootMeal = (DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs.Meal)meal.GetConstantValue();
                                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(rootMeal.id, context);

                                //TODO: add it to that day, how to tell?
                            }

                        }
                        */



            //instantiate the meal plan object that will be fully created and returned
            MealPlan mealPlanObject = new MealPlan();
            mealPlanObject.MealDays = new List<DaysMeal>();

            //each mealplan has a list of days, must add meals to each of those days
            //SUNDAY
            var sundayMeals = rootResponse.week.sunday;
            //each list of days has a list of meals
            List<Models.Entities.Meal> convertedSundayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in sundayMeals.meals)
            {
                //call the full meal DTO and convert to a full meal
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);

                //add to list of meals for sunday
                convertedSundayMeals.Add(convertedMeal);
            }
            //add the meal to the meal plan under sunday
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Sunday, Meals = convertedSundayMeals });

            //NOTE: Must repeat for each of the other days, due to nature of DTO, must do this manually
            //if time was not an issue, i would investigate how to improve this repetition of code

            //MONDAY
            var mondayMeals = rootResponse.week.monday;
            List<Models.Entities.Meal> convertedMondayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in mondayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);   //add to list of meals for sunday
                convertedMondayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Monday, Meals = convertedMondayMeals });

            //TUESDAY
            var tuesdayMeals = rootResponse.week.tuesday;
            List<Models.Entities.Meal> convertedTuesdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in tuesdayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                convertedTuesdayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Tuesday, Meals = convertedTuesdayMeals });

            //WEDNESDAY 
            var wednesdayMeals = rootResponse.week.wednesday;
            List<Models.Entities.Meal> convertedWednesdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in wednesdayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                convertedWednesdayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Wednesday, Meals = convertedWednesdayMeals });

            //THURSDAY
            var thursdayMeals = rootResponse.week.thursday;
            List<Models.Entities.Meal> convertedThursdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in thursdayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                convertedThursdayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Thursday, Meals = convertedThursdayMeals });

            //FRIDAY
            var fridayMeals = rootResponse.week.friday;
            List<Models.Entities.Meal> convertedFridayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in fridayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                convertedFridayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Friday, Meals = convertedFridayMeals });

            //SATURDAY
            var saturdayMeals = rootResponse.week.saturday;
            List<Models.Entities.Meal> convertedSaturdayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in saturdayMeals.meals)
            {
                var convertedMeal = await ConvertSpoonacularMealToFullMealAsync(meal.id, context);
                convertedSaturdayMeals.Add(convertedMeal);
            }
            mealPlanObject.MealDays.Add(new DaysMeal { Day = DayOfWeek.Saturday, Meals = convertedSaturdayMeals });

            //last step, return full converted meal plan
            return mealPlanObject;
            //}
            // catch (System.Exception)
            // {

            //     throw new Exception("An error has occured creating a Meal Plan from the Spoonacular meal plan");
            // }

        }
    }
}