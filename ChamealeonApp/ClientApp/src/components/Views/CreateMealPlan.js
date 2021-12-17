import React, { useState } from 'react';
import { Form, Button, Row, ListGroupItem, ListGroup } from 'react-bootstrap';
import axios from 'axios';
//R: https://react-bootstrap.github.io/components/forms/#forms-range
export default function CreateMealPlan() {
    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };


    let [restrictions, setRestrictions] = useState([]);
    let [textInputState, setTextInputState] = useState(null);

    function handleInputChange(e) {
        setTextInputState(e.target.value);
    }

    function addRestriction(newItem) {
        setRestrictions((prevState) => {
            const newRestrictions = [...prevState]

            newRestrictions.push(newItem);
            return newRestrictions;
        });
    }

    function makeCallToGenerateMealPlan() {
        const body = {
            "itemsToExclude": restrictions
        };
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        axios.post('http://localhost:5000/api/mealPlan/generateMealPlan/', body, config)
            .then(resp => alert(resp.statusText))
            .catch(err => {
                alert("No meal plan was created")
                console.error(err);
            });
    }

    return (

        <div>
            <h1>Create a new meal plan</h1>

            <div>
                <Form>
                    <Form.Group>
                        <Row className='align-items-center'>
                            <Form.Label>Add any ingredient restrictions:</Form.Label>
                            <Form.Control type="text" placeholder="Ingredient restriction" onChange={handleInputChange} />
                            <Button variant="primary" onClick={() => addRestriction(textInputState)}>
                                Add
                            </Button>
                        </Row>


                        <Form.Text className="text-muted">
                            Your plan will not include any:
                        </Form.Text>
                        <br></br>
                    </Form.Group>

                    <ListGroup>
                        {restrictions.map(food =>
                            <ListGroupItem key={Math.random()} className="text-muted">
                                {food}
                                <br></br>
                            </ListGroupItem>
                        )}
                    </ListGroup>

                    <Button variant="primary" onClick={makeCallToGenerateMealPlan}>
                        Generate meal plan
                    </Button>

                </Form>
            </div>

        </div >



    )
}