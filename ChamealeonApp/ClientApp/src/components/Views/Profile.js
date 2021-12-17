import React, { useState, useEffect } from 'react';
import { FloatingLabel, Form, Button } from 'react-bootstrap';
import { Label } from 'reactstrap';
import axios from 'axios';

//R: https://react-bootstrap.github.io/components/forms/#forms-floating-labels
export default function Profile() {
    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };

    const [inputs, setInputs] = useState({
        name: "",
        age: "",
        weight: "",
        height: "",
        calories: ""
    })

    const [originalData, setOriginalData] = useState({
        name: "",
        age: "",
        weight: "",
        height: "",
        calories: ""
    })

    useEffect(() => {
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        axios.get('http://localhost:5000/api/user/userDetails', config)
            .then(resp => {
                console.log(resp.data)
                const obj = {
                    name: resp.data.name,
                    age: resp.data.age,
                    weight: resp.data.weight,
                    height: resp.data.height,
                    calories: resp.data.calories,
                }
                setInputs(obj)
                setOriginalData(obj)
            }).then(

            )
            .catch(err => {
                console.error(err);
            });
    }, []);

    const handleInputChange = (event) => {
        const value = event.target.value;
        setInputs({
            ...inputs,
            [event.target.name]: value
        });
    }


    function updateUser() {
        const body = {
            "age": inputs.age,
            "weight": inputs.weight,
            "height": inputs.height,
            "calories": inputs.calories
        };
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        axios.put('http://localhost:5000/api/user/update', body, config)
            .then(resp => console.log(resp.statusText))
            .catch(err => {
                alert("Could not update user")
                console.error(err);
            });
    }

    return (
        < div >
            <h1>Details for {inputs.name}</h1>
            <>
                <Label>Age:</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={originalData.age}
                    className="mb-3"
                >
                    <Form.Control name="age" type="number" placeholder={inputs.age} value={inputs.age} onChange={handleInputChange} />
                </FloatingLabel>
            </>
            <>
                <Label>Height (cm):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={originalData.height}
                    className="mb-3"
                >
                    <Form.Control name="height" type="number" placeholder={inputs.height} value={inputs.height} onChange={handleInputChange} />
                </FloatingLabel>
            </>
            <>
                <Label>Weight (kg):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={originalData.weight}
                    className="mb-3"
                >
                    <Form.Control name="weight" type="number" placeholder={inputs.weight} value={inputs.weight} onChange={handleInputChange} />
                </FloatingLabel>
            </>
            <>
                <Label>Daily calorie goal (cals):</Label>
                <FloatingLabel
                    controlId="floatingInput"
                    label={originalData.calories}
                    className="mb-3"
                >
                    <Form.Control name="calories" type="number" placeholder={inputs.calories} value={inputs.calories} onChange={handleInputChange} />
                </FloatingLabel>
            </>
            <Button variant="primary" onClick={updateUser}>
                Save
            </Button>

        </div>


    )
}