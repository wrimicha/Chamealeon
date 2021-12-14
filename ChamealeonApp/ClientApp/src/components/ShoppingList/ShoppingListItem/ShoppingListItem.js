import React from 'react'
import '../../../styles/ShoppingListItem.css'

export default function ShoppingListItem(){
    return (
        <div>
        <div className="shopping-list-item">
            <p className="item-name">Tortilla Chips</p>
            <p className="price">$2.50</p>
        </div>
        <hr
        style={{
            color: 'grey',
            height: 5
        }}
    />
    </div>
    )
}

