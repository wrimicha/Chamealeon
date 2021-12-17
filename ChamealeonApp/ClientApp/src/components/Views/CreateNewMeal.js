import { useState } from "react"
import { Button, Input, InputGroup, InputGroupText } from "reactstrap"

const CreateNewMeal = () => {

    const [inputs, setInputs] = useState({

    })

    const handleInputChange = () => {

    }

    const handleSignUp = () => {

    }

    return (
        <div>
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Ingredients" value={inputs.ingredients} onChange={handleInputChange} placeholder="ingredients" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Cost" value={inputs.cost} onChange={handleInputChange} placeholder="cost" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="PrepTime" value={inputs.prepTime} onChange={handleInputChange} placeholder="prep time" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Instructions" value={inputs.instructions} onChange={handleInputChange} placeholder="instruction" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="name" value={inputs.name} onChange={handleInputChange} placeholder="name" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Calories" value={inputs.calories} onChange={handleInputChange} placeholder="calories" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Fat" value={inputs.fat} onChange={handleInputChange} placeholder="fat" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Protein" value={inputs.protein} onChange={handleInputChange} placeholder="protein" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Carbs" value={inputs.carbs} onChange={handleInputChange} placeholder="carbs" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Sodium" value={inputs.sodium} onChange={handleInputChange} placeholder="sodium" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    {/* <HiOutlineMail /> */}
                </InputGroupText>
                <Input name="Sugar" value={inputs.sugar} onChange={handleInputChange} placeholder="sugar" />
            </InputGroup>
            <br />

            <Button
                color="success"
                outline
                onClick={handleSignUp}
            >
                Submit Meal
            </Button>
        </div>
    )
}

export default CreateNewMeal