import { Button } from "reactstrap"
import axios from "axios";

const Login = () => {
    return (
        <div>
            <p>email</p>
            <input></input>
            <p>pass</p>
            <input></input>
            <Button onClick={() => {
                console.log("hi")
                axios.post("http://localhost:5000/api/User/Login",
                    {
                        email: "a@b.c",
                        password: "amirA123@@"
                    }, 
                    { withCredentials: true })
                    .then(result => console.log(result))
                    .catch(err => console.log(err))
            }}>Click me</Button>
        </div >
    )
}


export default Login