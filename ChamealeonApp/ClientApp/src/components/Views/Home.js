import React from 'react';
import MealCard from '../MealCard/MealCard'
import '../../styles/Home.css'


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
      </div>
    </div>
  );

}
export default Home