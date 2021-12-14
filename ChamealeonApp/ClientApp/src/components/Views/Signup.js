import  {Button} from "reactstrap"
import axios from "axios"

const Signup = () => {
    return (
        <Button onClick={() => {
            axios.post("http://localhost/api/user/signup",)
        }}>Click me</Button>
    )
}

export default Signup