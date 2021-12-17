import React, { useState, useRef, useEffect } from "react";
import { Modal, Button, Form, Alert } from "react-bootstrap";
import { Link } from "react-router-dom";
import MealCardPopup from '../MealCard/MealCardPopup'
import '../../styles/MealCard.css'

const SwapMealModal = ({ modalShow, setShow }) => {
  //const [show, setShow] = useState(true);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

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