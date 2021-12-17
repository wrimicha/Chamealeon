import React, { useState, useRef, useEffect } from "react";
import { Modal, Button, Form, Alert } from "react-bootstrap";
import { Link } from "react-router-dom";
import MealCardPopup from '../MealCard/MealCardPopup'
import '../../styles/MealCard.css'

const SwapMealModal = ({ modalShow, setShow }) => {
  //const [show, setShow] = useState(true);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  // const handleClose = () => {
  //   setShow(false);
  //   setError("");
  // }

  const handleSubmit = async (e) => {
    // e.preventDefault(); //not needed unless using the form submit?

    // //error validation
    // if (emailRef.current.value.trim() === "") {
    //   return setError("Email cannot be blank");
    // }
    // if (passwordRef.current.value.trim() === "") {
    //   return setError("Password cannot be blank");
    // }

    // setError("");
    // setLoading(true);
    // login(emailRef.current.value, passwordRef.current.value).then(
    //   () => handleClose()
    //   // .then(() => history.push("/"))
    // ).catch((e) => {
    //   setError(e.message);
    // });
    // addAdminRole();
    // setLoading(false);
  };

  return (
    <>
      <Modal
        // style={{width: '100%!important'}}
        dialogClassName="my-modal"
        centered
        dialogClassName="mainModal"
        show={modalShow}
        onHide={() => setShow(false)}
      >
        <div className="formMain">
        <div style={{ display: 'flex', flexWrap: "wrap", width: '100%'}}>
            <MealCardPopup/>
            <MealCardPopup/>
            <MealCardPopup/>
            <MealCardPopup/>
            <MealCardPopup/>
            <MealCardPopup/>
            
          </div>
        </div>
      </Modal>
      <auth />
    </>
  );
};

export default SwapMealModal;