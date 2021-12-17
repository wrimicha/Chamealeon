import { useState } from "react"
import { BiTimer } from "react-icons/bi"
import { GiMeat, GiFoodTruck, GiSlicedBread, GiSaltShaker, GiSugarCane } from "react-icons/gi"
import { FaHamburger, FaUtensilSpoon } from "react-icons/fa"
import { VscSymbolNamespace } from "react-icons/vsc"
import { MdOutlineIntegrationInstructions, MdPriceChange } from "react-icons/md"
import { Button, Input, InputGroup, InputGroupText } from "reactstrap"
import axios from "axios"

const CreateNewMeal = () => {
    const token = localStorage.getItem("jwt");

    const [inputs, setInputs] = useState({
        ingredients: "",
        cost: "",
        prepTime: "",
        instructions: "",
        name: "",
        calories: "",
        fat: "",
        sodium: "",
        carbs: "",
        protein: "",
        sugar: ""

    })

    const handleInputChange = (event) => {
        const value = event.target.value;
        setInputs({
            ...inputs,
            [event.target.name]: value
        });
    }

    const handleCreate = () => {
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };
        axios.post("http://localhost:5000/api/meal",
            {
                ingredients: inputs.nutritionInfo,
                cost: inputs.cost,
                prepTime: inputs.prepTime,
                instructions: inputs.instructions,
                name: inputs.name,
                nutritionInfo: {
                    calories: inputs.calories,
                    fat: inputs.fat,
                    sodium: inputs.sodium,
                    carbs: inputs.carbs,
                    protein: inputs.protein,
                    sugar: inputs.sugar
                }
            }, config).then(result => console.log(result.status))
    }

    return (
        <div>
            <InputGroup>
                <InputGroupText>
                    <FaUtensilSpoon />
                </InputGroupText>
                <Input name="ingredients" value={inputs.ingredients} onChange={handleInputChange} placeholder="ingredients" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <MdPriceChange />
                </InputGroupText>
                <Input name="cost" value={inputs.cost} onChange={handleInputChange} placeholder="cost" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <BiTimer />
                </InputGroupText>
                <Input name="prepTime" value={inputs.prepTime} onChange={handleInputChange} placeholder="prep time" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <MdOutlineIntegrationInstructions />
                </InputGroupText>
                <Input name="instructions" value={inputs.instructions} onChange={handleInputChange} placeholder="instruction" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <VscSymbolNamespace />
                </InputGroupText>
                <Input name="name" value={inputs.name} onChange={handleInputChange} placeholder="name" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <GiFoodTruck />
                </InputGroupText>
                <Input name="calories" value={inputs.calories} onChange={handleInputChange} placeholder="calories" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <FaHamburger />
                </InputGroupText>
                <Input name="fat" value={inputs.fat} onChange={handleInputChange} placeholder="fat" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <GiMeat />
                </InputGroupText>
                <Input name="protein" value={inputs.protein} onChange={handleInputChange} placeholder="protein" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <GiSlicedBread />
                </InputGroupText>
                <Input name="carbs" value={inputs.carbs} onChange={handleInputChange} placeholder="carbs" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <GiSaltShaker />
                </InputGroupText>
                <Input name="sodium" value={inputs.sodium} onChange={handleInputChange} placeholder="sodium" />
            </InputGroup>
            <br />
            <InputGroup>
                <InputGroupText>
                    <GiSugarCane />
                </InputGroupText>
                <Input name="sugar" value={inputs.sugar} onChange={handleInputChange} placeholder="sugar" />
            </InputGroup>
            <br />

            <Button
                color="success"
                outline
                onClick={handleCreate}
            >
                Submit Meal
            </Button>
        </div>
    )
}

export default CreateNewMeal