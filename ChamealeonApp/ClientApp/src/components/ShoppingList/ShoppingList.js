import React from 'react'
import ShoppingListItem from '../ShoppingList/ShoppingListItem/ShoppingListItem'
import '../../styles/ShoppingList.css'
import { BiPlus } from "react-icons/bi";


export default function ShoppingList() {
    return (

        <div>

            <div className="plus-button-group">
                <button className="add-item">
                    <BiPlus/>
                </button>
            </div>
            

            <div className="shopping-list-container">
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>
                <ShoppingListItem/>

            </div>

        </div>

       
    )
}
