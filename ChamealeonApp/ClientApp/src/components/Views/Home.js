import React, { useState } from "react";
import { Button } from "react-bootstrap";
import MealCard from '../MealCard/MealCard'
import '../../styles/Home.css'
import SwapMealModal from './SwapMealModal';
import SwapModal from "../Views/SwapMealModal";

const Home = () => {
  
  const [modalShow, setModalShow] = useState(false);


  return (
    <div>
      <SwapModal
          modalShow={modalShow}
          setShow={setModalShow}
          >
          {/* <SwapMealModal /> */}
      </SwapModal>
      {/* <div show={false}>HELLO</div> */}

      <div className="meal-container">
        <MealCard 
        modalShow={modalShow}
        setShow={setModalShow} />
        <MealCard
        modalShow={modalShow}
        setShow={setModalShow}/>
        <MealCard
        modalShow={modalShow}
        setShow={setModalShow} />
        <MealCard 
        modalShow={modalShow}
        setShow={setModalShow}/>
        <MealCard
        modalShow={modalShow}
        setShow={setModalShow}/>
        <MealCard
        modalShow={modalShow}
        setShow={setModalShow} />
        
      </div>
    </div>
  );

  
}
export default Home