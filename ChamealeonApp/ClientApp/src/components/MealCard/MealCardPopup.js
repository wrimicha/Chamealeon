import React, { useState, useRef, useEffect } from "react";
import { Modal, Button, Form, Alert } from "react-bootstrap";
import { Link } from "react-router-dom";
import "../../styles/MealCard.css"
import { IoSwapHorizontalOutline } from "react-icons/io5"


const SwapMealModal = ({show, setShow }) => {

return(
    <>
      <div className="meal-card-group">
             {/* <div className="meal-card-thumb" style={{ backgroundImage: "url(" + props.bg + ")" }}>
             </div> */}
            
            <div className="meal-card-thumb" style={{ backgroundImage: "url(https://www.pexels.com/photo/1640777/download/)" }}>
            </div>
        
             <div className="meal-card-text">
                 <p className="card-title">Carrot & Rice with Bell Pepper</p>
               {/* <a href="#">{props.name}</a> */}
               <div className="macros-container">

                     <div className="macro-group">
                         <p className="macro-title">Calories</p>
                         <p className="macro-value">800</p>
                     </div>
                     <div className="macro-group">
                         <p className="macro-title">Carbs</p>
                         <p className="macro-value">800</p>
                         <p className="macro-title">Protein</p>
                     </div>
                     <div className="macro-group">
                         <p className="macro-value">800</p>
                     </div>
                     <div className="macro-group">
                        <p className="macro-title">Fat</p>
                        <p className="macro-value">800</p>
                     </div>

                 </div>
             </div>
       </div>
    </>
  );
};

export default SwapMealModal;