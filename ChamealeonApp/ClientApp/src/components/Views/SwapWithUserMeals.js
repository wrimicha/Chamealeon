import React, { useState, useEffect } from 'react';
import '../../styles/SwapMeal.css'
import axios from 'axios';
import { Button, Input, InputGroupText, InputGroup } from "reactstrap"
import { ListGroup, ListGroupItem } from 'react-bootstrap';

export default function SwapWithUserMeal() {
    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };

    const [displayedMeals, setDisplayedMeals] = useState([])
    const [dayIndex, setDayIndex] = useState(null)
    const [mealIndex, setMealIndex] = useState(null)
    const [mealId, setMealId] = useState(null)

    function handleDayInputChange(e) {
        setDayIndex(e.target.value);
    }
    function handleMealInputChange(e) {
        setMealIndex(e.target.value);
    }
    function handleMealIdChange(e) {
        setMealId(e.target.value);
    }

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

    function swapSelectedMeal() {
        //api
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        console.log(mealId)
        console.log(dayIndex)

        console.log(mealIndex)

        axios.put(`http://localhost:5000/api/mealPlan/updateMealPlanWithUserMeal/${mealId}?day=${dayIndex}&mealIndexInDay=${mealIndex}`, {}, config)
            .then(resp => {
                alert("Swapped meal!")
            })
            .catch(err => {
                alert("Invalid index for day/meal");
            });
    }

    return (
        <div>
            <InputGroup>
                <InputGroupText>
                </InputGroupText>
                <Input name="day" value={dayIndex} onChange={handleDayInputChange} placeholder="Day index" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                </InputGroupText>
                <Input name="meal" value={mealIndex} onChange={handleMealInputChange} placeholder="Meal index" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                </InputGroupText>
                <Input name="mealId" value={mealId} onChange={handleMealIdChange} placeholder="Meal ID to replace with" />
            </InputGroup>
            <br />
            <Button variant="primary" onClick={swapSelectedMeal}>
                Swap meal

            </Button>

            <div className="meal-container">
                {displayedMeals.map((data) =>
                    <ListGroup key={data.id}>
                        <ListGroupItem>
                            Meal ID: {data.id}
                        </ListGroupItem>
                        <ListGroupItem>
                            Meal name: {data.name}
                        </ListGroupItem>
                        <ListGroupItem>
                            Nutrition: {data.nutritionInfo.calories} calories, {data.nutritionInfo.fat}g of fat, {data.nutritionInfo.protein}g of protein & {data.nutritionInfo.carbs}g of carbohydrates
                        </ListGroupItem>
                        <ListGroupItem>
                            Instructions: {data.instructions}
                        </ListGroupItem>
                        <br />
                    </ListGroup>)}
            </div>
        </div>
    );

}