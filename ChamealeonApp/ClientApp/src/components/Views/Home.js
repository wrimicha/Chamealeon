import React from 'react';
import MealCard from '../MealCard/MealCard'
import '../../styles/Home.css'
import SwapMealModal from './SwapMealModal';


const Home = () => {
  
  return (
    <div>
      <div className="meal-container">
        <MealCard />
        <MealCard />
        <MealCard />
        <MealCard />
        <MealCard />
        <MealCard />
        {/* <Modal>
          <SwapMealModal />
        </Modal> */}
      </div>
    </div>
  );

}
export default Home