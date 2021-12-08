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
        public static Models.Entities.Meal ConvertRootMealToMeal(Models.DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs.Meal spoonacularMeal)
        {
            //create a meal object out of this meal and save to database
            return new Models.Entities.Meal
            {
                //Ingredients list filled by Amir
                SpoonacularMealId = spoonacularMeal.id,
                //TODO: ASK?
                MealType = spoonacularMeal.imageType,
                // Cost = ,
                PrepTime = spoonacularMeal.readyInMinutes,
                // Instructions = ,
                Name = spoonacularMeal.title,
                ImageUrl = spoonacularMeal.sourceUrl,
                //NutritionInfo = , might be amir?
            };
        }

        //function to create a meal plan out of the root response
        public static MealPlan ConvertRootDTOToMealPlan(Root rootResponse)
        {
            MealPlan mealPlanObject = new MealPlan();

            //each mealplan has a list of days
            //sunday
            var sundayMeals = rootResponse.week.sunday;

            //each list of days has a list of meals

            //TODO: each meal has nutrients and ingredients-> amir?
            List<Models.Entities.Meal> convertedSundayMeals = new List<Models.Entities.Meal>();
            foreach (var meal in sundayMeals.meals)
            {
                //call convert rootomeal
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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
                var convertedMeal = ConvertRootMealToMeal(meal);

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