import React from 'react';
import { Form, Button, Row } from 'react-bootstrap';
//R: https://react-bootstrap.github.io/components/forms/#forms-range
export default function CreateMealPlan() {
    var restrictions = ["tomato", "olives"];

    return (

        <div>
            <h1>Create a new meal plan</h1>

            <div>
                <Form>
                    <Form.Group>
                        <Form.Label>Enter daily maximum calorie limit</Form.Label>
                        <Form.Control type="number" placeholder="Enter calorie limit" />

                    </Form.Group>

                    <Form.Group>
                        <Form.Label>Select a diet</Form.Label>
                        <br></br>
                        <Form.Check inline label="Vegan" name="diet" type='radio' id="vegan" />
                        <Form.Check inline label="Gluten Free" name="diet" type='radio' id="glutenfree" />
                        <Form.Check inline label="Paleo" name="diet" type='radio' id="paleo" />
                        <Form.Check inline label="Ketogenic" name="diet" type='radio' id="ketogenic" />
                        <Form.Check inline label="Pescetarian" name="diet" type='radio' id="pescetarian" />
                        <Form.Check inline label="Vegetarian" name="diet" type='radio' id="vegetarian" />
                        <Form.Check inline label="Primal" name="diet" type='radio' id="primal" />

                    </Form.Group>

                    <Form.Group>
                        <Row className='align-items-center'>
                            <Form.Label>Add any ingredient restrictions</Form.Label>
                            <Form.Control type="text" placeholder="Ingredient restriction" />

                            <Button variant="primary" type="submit">
                                Add
                            </Button>
                        </Row>


                        <Form.Text className="text-muted">
                            Your plan will not include any:
                        </Form.Text>
                        <br></br>




                        {restrictions.map(food =>
                            <Form.Text className="text-muted" Text={food}>
                                {food}
                                <br></br>
                            </Form.Text>
                        )}

                    </Form.Group>

                    <Button variant="primary" type="submit">
                        Generate meal plan
                    </Button>

                </Form>
            </div>

        </div >



    )
}