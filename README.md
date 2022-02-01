# Chamealeon

<img width="1280" alt="Screen Shot 2022-01-11 at 7 32 04 PM" src="https://user-images.githubusercontent.com/60019847/151917254-f155b4d0-9014-4b8d-b3fa-6d399b9ffffc.png">
<img width="1278" alt="Screen Shot 2022-01-11 at 7 47 15 PM" src="https://user-images.githubusercontent.com/60019847/151917273-f79f9a96-d197-4f1c-a81e-c0b6e79e7c48.png">
<img width="1280" alt="Screen Shot 2022-01-11 at 8 07 00 PM" src="https://user-images.githubusercontent.com/60019847/151917302-ec5f4030-b070-412e-810c-ef853b1071ad.png">


# Project Overview
This project was created for our fifth semester Enterprise Software Systems final group project. The project is a health-based web application. The core functionality of the application is to generate meal plans for the week. There are 3 meals per day which are breakfast, lunch, and dinner respectively, for a total of 7 days or 21 meals. These meal plans are unique to each user as it considers the dietary needs/preferences as specified by the user for example if they are vegetarian. With this being said however, if the user does not like a particular meal that is generated, they will have the option to replace it with another meal. 

A list of potential replacements will be provided to the user which match their dietary needs, or they can add their own meals if they wish. The user can then enter the ingredients and quantity of ingredients used in the recipe, which would generate nutritional information such as macros for the given recipe. Each meal, whether it’s auto generated or input by the user, would list the nutritional information at a quick glance. To make the meal preparation easier, we will have a separate page that will display a full shopping list of ingredients based on the meals that are generated. There will also be a summary of the nutritional information for the week such as total calories, protein, fats, carbohydrates etc. so they can see if they are meeting their nutritional needs or if they are lacking in any area.  

# Technologies Used 

The solution will be a progressive web app single page application made in React.JS that pairs with a backend ASP.NET API. The database for the solution will be Entity Framework Core. Most of the API calls consist of calls to the ASP.NET controller and calls to the extensive Spoonacular API, and for that AXIOS will be used to familiarize ourselves with it. Spoonacular API allows us to make detailed requests that generate responses for meals. It obtains the meals from other food websites and includes URLs in the response to go to the direct recipe itself.
