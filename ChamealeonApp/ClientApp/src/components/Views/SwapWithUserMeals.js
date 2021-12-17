import React, { useState, useEffect } from 'react';
import MealCard from '../MealCard/MealCard'
import '../../styles/SwapMeal.css'
import axios from 'axios';

export default function SwapWithUserMeal({ dayIndex, mealIndex }) {
    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };

    const [displayedMeals, setDisplayedMeals] = useState([])

    useEffect(() => {
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        axios.get('http://localhost:5000/api/swapmeal/displayUserMeals', config)
            .then(resp => {
                console.log(resp.data)
                setDisplayedMeals(resp.data)
            }).then(

            )
            .catch(err => {
                console.error(err);
            });
    }, [])

    function swapSelectedMeal(e) {
        //api
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        //TODO: get the id of the clicked meal
        const userMealId = e.target.id.value;
        axios.put(`http://localhost:5000/api/mealPlan/updateMealPlanWithUserMeal/${userMealId}?day=${dayIndex}&mealIndexInDay=${mealIndex}`, config)
            .then(resp => {
                alert("Swapped meal")
            })
            .catch(err => {
                console.error(err);
            });
    }

    return (
        <div>
            <div className="meal-container">
                {displayedMeals.map((data) => <MealCard></MealCard>)}
            </div>
        </div>
    );

}