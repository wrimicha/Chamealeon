import React from 'react';
import { FloatingLabel, Form, Button } from 'react-bootstrap';
import { Label } from 'reactstrap';

//R: https://react-bootstrap.github.io/components/forms/#forms-floating-labels
export default function Profile() {
    const user = {
        "name": "Bob Jones",
        "calories": 3000,
        "height": 170,
        "weight": 70,
        "age": 21,
        "diet": "Vegetarian"
    }
    return (
        < div >
            <h1>Details for {user.name}</h1>
            <>
                <Label>Age:</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={user.age}
                    className="mb-3"
                >
                    <Form.Control type="number" placeholder={user.age} />
                </FloatingLabel>
            </>
            <>
                <Label>Height (cm):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={user.height}
                    className="mb-3"
                >
                    <Form.Control type="number" placeholder={user.height} />
                </FloatingLabel>
            </>
            <>
                <Label>Weight (kg):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={user.weight}
                    className="mb-3"
                >
                    <Form.Control type="number" placeholder={user.weight} />
                </FloatingLabel>
            </>
            <>
                <Label>Daily calorie goal (cals):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={user.calories}
                    className="mb-3"
                >
                    <Form.Control type="number" placeholder={user.calories} />
                </FloatingLabel>
            </>
            <Form.Group>
                <Form.Label>Diet:</Form.Label>
                <br></br>
                <Form.Check checked inline label="Vegan" name="vegan" type='radio' id="vegan" />
                <Form.Check inline label="Gluten Free" name="glutenfree" type='radio' id="glutenfree" />
                <Form.Check inline label="Paleo" name="paleo" type='radio' id="paleo" />
                <Form.Check inline label="Ketogenic" name="ketogenic" type='radio' id="ketogenic" />
                <Form.Check inline label="Pescetarian" name="pescetarian" type='radio' id="pescetarian" />
                <Form.Check inline label="Vegetarian" name="vegetarian" type='radio' id="vegetarian" />
                <Form.Check inline label="Primal" name="primal" type='radio' id="primal" />

            </Form.Group>

            <Button variant="primary" type="submit">
                Save
            </Button>

        </div>


    )
}