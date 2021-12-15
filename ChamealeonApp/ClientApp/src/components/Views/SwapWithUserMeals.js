import React from 'react';
import MealCard from '../MealCard/MealCard'
import '../../styles/SwapMeal.css'
import { Button } from 'react-bootstrap';


export default function SwapWithUserMeal() {
    //make a request to get logged in user's meals
    //paginate? randomized?
    function clickedMeal() {
        alert("You clicked this meal")
    }

    return (
        <div>
            <div className="meal-container">
                <div>
                    <MealCard onClick={clickedMeal}></MealCard>
                </div>
                <Button onClick={clickedMeal}>
                    <MealCard />
                </Button>
                <div>
                    <MealCard onClick={clickedMeal}></MealCard>
                </div>
                <div>
                    <MealCard onClick={clickedMeal}></MealCard>
                </div>
            </div>
        </div>
    );

}