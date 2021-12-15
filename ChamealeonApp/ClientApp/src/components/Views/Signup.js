import { Button, Input, InputGroupText, InputGroup } from "reactstrap"
import axios from "axios"
import { HiOutlineMail, HiFingerPrint, HiUser, HiOutlineAnnotation, HiCalendar } from 'react-icons/hi'
import { FaToilet, FaWeight } from "react-icons/fa"
import { MdFoodBank, MdHeight, MdFastfood } from "react-icons/md"
import { useState } from "react"
const Signup = () => {

    const [inputs, setInputs] = useState({
        email: "",
        password: "",
        username: "",
        name: "",
        age: "",
        gender: "",
        diet: "",
        weight: "",
        height: "",
        targetCalories: ""
    })
    const handleSignUp = (event) => {
        axios.post("http://localhost:5000/api/User/Register",
            {
                email: inputs.email,
                password: inputs.password,
                username: inputs.username,
                name: inputs.name,
                age: inputs.age,
                gender: inputs.gender,
                diet: inputs.diet,
                weight: inputs.weight,
                height: inputs.height,
                personalNutritionalInformationGoal: {
                    calories: inputs.targetCalories
                }
            })
            .then(result => console.log(result))
            .catch(err => console.log(err))
    }

    const handleInputChange = (event) => {
        const value = event.target.value;
        setInputs({
            ...inputs,
            [event.target.name]: value
        });
    }
    return (
        <div>
            <InputGroup>
                <InputGroupText>
                    <HiOutlineMail />
                </InputGroupText>
                <Input name="email" value={inputs.email} onChange={handleInputChange} placeholder="email" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <HiFingerPrint />
                </InputGroupText>
                <Input name="password" value={inputs.password} onChange={handleInputChange} placeholder="password" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <HiUser />
                </InputGroupText>
                <Input name="username" value={inputs.username} onChange={handleInputChange} placeholder="username" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <HiOutlineAnnotation />
                </InputGroupText>
                <Input name="name" value={inputs.name} onChange={handleInputChange} placeholder="name" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <HiCalendar />
                </InputGroupText>
                <Input name="age" value={inputs.age} onChange={handleInputChange} placeholder="age" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <FaToilet />
                </InputGroupText>
                <Input name="gender" value={inputs.gender} onChange={handleInputChange} placeholder="gender" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <MdFoodBank />
                </InputGroupText>
                <Input name="diet" value={inputs.diet} onChange={handleInputChange} placeholder="diet" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <FaWeight />
                </InputGroupText>
                <Input name="weight" value={inputs.weight} onChange={handleInputChange} placeholder="weight" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <MdHeight />
                </InputGroupText>
                <Input name="height" value={inputs.height} onChange={handleInputChange} placeholder="height" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <MdFastfood />
                </InputGroupText>
                <Input name="targetCalories" value={inputs.targetCalories} onChange={handleInputChange} placeholder="target calories" />
            </InputGroup>
            <br />
            <Button
                color="success"
                outline
                onClick={handleSignUp}
            >
                Signup
            </Button>
        </div>
    )
}

export default Signup