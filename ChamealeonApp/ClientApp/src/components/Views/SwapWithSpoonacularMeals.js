import React from 'react';
import MealCard from '../MealCard/MealCard'
import '../../styles/Home.css'
import { Button, InputGroup, FormControl, FormGroup, Form } from 'react-bootstrap';

//R: https://react-bootstrap.github.io/components/input-group/#input-group-sizes

export default function SwapWithSpoonacularMeals() {
    //make a request to get logged in user's meals
    //paginate? randomized?
    function clickedMeal() {
        alert("You clicked this meal")
    }

    return (
        <div>
            <div>
                <Form>
                    <FormGroup>
                        <InputGroup size="lg">
                            <FormControl placeholder='Meal query' aria-label="Large" aria-describedby="inputGroup-sizing-sm" />
                        </InputGroup>




                    </FormGroup>

                    <Form.Group>
                        <Form.Label>Select a meal type</Form.Label>
                        <br></br>
                        <Form.Check inline label="Vegan" name="meal" type='radio' id="vegan" />
                        <Form.Check inline label="Gluten Free" name="meal" type='radio' id="glutenfree" />
                        <Form.Check inline label="Paleo" name="meal" type='radio' id="paleo" />
                        <Form.Check inline label="Ketogenic" name="meal" type='radio' id="ketogenic" />
                        <Form.Check inline label="Pescetarian" name="meal" type='radio' id="pescetarian" />
                        <Form.Check inline label="Vegetarian" name="meal" type='radio' id="vegetarian" />
                        <Form.Check inline label="Primal" name="meal" type='radio' id="primal" />

                    </Form.Group>

                    <Form.Group>
                        <Form.Label>Select a diet</Form.Label>
                        <br></br>
                        <Form.Check inline label="Main Course" name="diet" type='radio' id="maincourse" />
                        <Form.Check inline label="Side Dish" name="diet" type='radio' id="sidedish" />
                        <Form.Check inline label="Dessert" name="diet" type='radio' id="dessert" />
                        <Form.Check inline label="Appetizer" name="diet" type='radio' id="appetizer" />
                        <Form.Check inline label="Salad" name="diet" type='radio' id="salad" />
                        <Form.Check inline label="Breakfast" name="diet" type='radio' id="breakfast" />
                        <Form.Check inline label="Soup" name="diet" type='radio' id="soup" />
                        <Form.Check inline label="Snack" name="diet" type='radio' id="snack" />

                    </Form.Group>
                    <Button variant="primary" type="submit">
                        Search Spoonacular
                    </Button>
                </Form>

            </div>


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