
import React, { useState, useRef, useEffect } from "react";
import { Modal, Button, Form, Alert } from "react-bootstrap";
import { Link } from "react-router-dom";
import "../../styles/MealCard.css"
import { IoSwapHorizontalOutline, IoTrash } from "react-icons/io5"
import axios from 'axios';


const SwapMealModal = ({id, mealDay, mealIndex, show, setShow, image, title, cals, carbs, protein, fat}) => {

  const handleDelete = () => {

    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };

    axios.delete(`http://localhost:5000/api/mealplan/removeMealFromMealPlan?mealDay=${mealDay}&mealIndex=${mealIndex}`, config)
      .then(result => {
        console.log(result);
      })   
      .catch(err => {
          console.error(err);
      });
  }

//   const handleSubmit = async (e) => {
//     e.preventDefault(); //not needed unless using the form submit?

//     //error validation
//     if (emailRef.current.value.trim() === "") {
//       return setError("Email cannot be blank");
//     }
//     if (passwordRef.current.value.trim() === "") {
//       return setError("Password cannot be blank");
//     }

//     setError("");
//     setLoading(true);
//     login(emailRef.current.value, passwordRef.current.value).then(
//       () => handleClose()
//       // .then(() => history.push("/"))
//     ).catch((e) => {
//       setError(e.message);
//     });
//     addAdminRole();
//     setLoading(false);
//   };

return (
    <>
      <div className="meal-card-group">
             {/* <div className="meal-card-thumb" style={{ backgroundImage: "url(" + props.bg + ")" }}>
             </div> */}
            
            <div className="meal-card-thumb" style={{ backgroundImage: `url(${image})` }}>
              <Button className="modal-button" onClick={() => show ? setShow(false) : setShow(true)}><IoSwapHorizontalOutline /></Button>
              <Button className="modal-button" onClick={() => handleDelete()}><IoTrash /></Button>
            </div>
        
             <div className="meal-card-text">
                 <p className="card-title">{title}</p>
               {/* <a href="#">{props.name}</a> */}
               <div className="macros-container">

                     <div className="macro-group">
                         <p className="macro-title">Calories</p>
                         <p className="macro-value">{cals}</p>
                     </div>
                     <div className="macro-group">
                         <p className="macro-title">Carbs</p>
                         <p className="macro-value">{carbs}</p>
                     </div>
                     <div className="macro-group">
                         <p className="macro-title">Protein</p>
                         <p className="macro-value">{protein}</p>
                     </div>
                     <div className="macro-group">
                        <p className="macro-title">Fat</p>
                        <p className="macro-value">{fat}</p>
                     </div>

                 </div>
             </div>
       </div>
    </>
  );
};

export default SwapMealModal;






















// import React from 'react'
// import '../../styles/MealCard.css'
// import { Button } from "react-bootstrap";

// export default function MealCard({modalShow, setShow }) {
//     return (
//         <div className="meal-card-group">
//             {/* <div className="meal-card-thumb" style={{ backgroundImage: "url(" + props.bg + ")" }}>
//             </div> */}
            
//             <Button onClick = {setShow(true)}>SHOW</Button>
//             <div className="meal-card-thumb" style={{ backgroundImage: "url(https://www.pexels.com/photo/1640777/download/)" }}></div>
//             <div className="meal-card-text">
//                 <p className="card-title">Carrot & Rice with Bell Pepper</p>
//                 {/* <a href="#">{props.name}</a> */}
//                 <div className="macros-container">
//                     <div className="macro-group">
//                         <p className="macro-title">Calories</p>
//                         <p className="macro-value">800</p>
//                     </div>
//                     <div className="macro-group">
//                         <p className="macro-title">Carbs</p>
//                         <p className="macro-value">800</p>
//                     </div>
//                     <div className="macro-group">
//                         <p className="macro-title">Protein</p>
//                         <p className="macro-value">800</p>
//                     </div>
//                     <div className="macro-group">
//                         <p className="macro-title">Fat</p>
//                         <p className="macro-value">800</p>
//                     </div>

//                 </div>
//             </div>
//         </div>
//     )
// }
