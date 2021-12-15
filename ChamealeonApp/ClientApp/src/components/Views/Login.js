import { Button, Input, InputGroup, InputGroupText } from "reactstrap"
import axios from "axios";
import { HiFingerPrint, HiOutlineMail } from "react-icons/hi";
import { useState } from "react";

const Login = ({authHook}) => {
    const [inputs, setInputs] = useState({
        email: "",
        password: "",
    })
    const handleLogin = (event) => {
        axios.post("http://localhost:5000/api/User/Login",
            {
                email: inputs.email,
                password: inputs.password,
            }, { withCredentials: true })
            .then(result => {
                localStorage.setItem("jwt", result.data)
            })
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
            <Button
                color="success"
                outline
                onClick={handleLogin}
            >
                Login
            </Button>
        </div >
    )
}


export default Login