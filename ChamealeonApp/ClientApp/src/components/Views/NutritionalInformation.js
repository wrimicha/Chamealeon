import React, { useState, useEffect } from 'react';
import {
    Chart as ChartJS, CategoryScale,
    LinearScale,
    BarElement, ArcElement, Tooltip, Legend
} from 'chart.js';
import { Pie, Bar } from 'react-chartjs-2';
import '../../styles/NutritionalInformation.css'
import { DropdownButton, Dropdown } from 'react-bootstrap';
import axios from 'axios';

// Reference: https://www.chartjs.org/docs/latest/charts/doughnut.html
//https://react-chartjs-2.netlify.app/examples/pie-chart
//https://react-chartjs-2.netlify.app/examples/vertical-bar-chart
//https://react-bootstrap.github.io/components/dropdowns/
ChartJS.register(CategoryScale,
    LinearScale,
    BarElement,
    ArcElement, Tooltip, Legend);
export default function NutritionalInformation() {
    const token = localStorage.getItem("jwt");
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };
    const userName = "Bob Jones";
    const options = {
        responsive: true,
        // maintainAspectRatio: false
    };
    //state
    let [day, setDay] = useState("Sunday");
    let [weekCalories, setWeekCalories] = useState(0);
    let [dayCalories, setDayCalories] = useState(0);
    let [weekData, setWeekData] = useState([0, 0, 0, 0, 0, 0]);
    let [dataDay, setDataDay] = useState([0, 0, 0, 0, 0, 0]);
    const nutritionDataWeek = {
        labels: ['Carbohydrates', 'Protein', 'Fat', 'Sodium', 'Sugar'],
        datasets: [
            {
                //TODO: from API call
                label: userName,
                data: weekData,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                ],
                borderWidth: 2,
            },
        ],
    };
    const nutritionDataDay = {
        labels: ['Carbohydrates', 'Protein', 'Fat', 'Sodium', 'Sugar'],
        datasets: [
            {
                //TODO: from API call
                label: userName,
                data: dataDay,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                ],
                borderWidth: 2,
            },
        ],
    };

    function getWeeklyInfo() {
        //Reference; https://stackabuse.com/making-asynchronous-http-requests-in-javascript-with-axios/
        axios.get('http://localhost:5000/api/nutrition/weeklyInformation', config)
            .then(resp => {
                console.log(resp.data);

                setWeekCalories(resp.data.calories);
                setWeekData([resp.data.carbs, resp.data.protein, resp.data.fat, resp.data.sodium / 1000, resp.data.sugar]);
            })
            .catch(err => {
                console.error(err);
            });

    }
    function getDailyInfo() {
        console.log(day)
        axios.get(`http://localhost:5000/api/nutrition/dailyInformation?day=${day}`, config)
            .then(resp => {
                console.log(resp.data);

                setDayCalories(resp.data.calories);
                setDataDay([resp.data.carbs, resp.data.protein, resp.data.fat, resp.data.sodium / 1000, resp.data.sugar]);
            })
            .catch(err => {
                console.error(err);
            });

    }
    useEffect(() => {
        getWeeklyInfo();
        getDailyInfo();
    }, [])

    //whenever day changes, make a new call
    useEffect(() => {
        getDailyInfo();
    }, [day])

    return (

        <div>
            <div>
                <p>Total Calories: {weekCalories}</p>
                <div className='flex-box'>
                    <Pie options={options} data={nutritionDataWeek} />
                    <Bar options={options} data={nutritionDataWeek} />
                </div>
            </div>
            <div>
                <DropdownButton title={day}>
                    <Dropdown.Item onClick={() => setDay("Sunday")} >Sunday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Monday")}>Monday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Tuesday")}>Tuesday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Wednesday")}>Wednesday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Thursday")}>Thursday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Friday")}>Friday</Dropdown.Item>
                    <Dropdown.Item onClick={() => setDay("Saturday")}>Saturday</Dropdown.Item>
                </DropdownButton>

                <p>Total Calories for {day}: {dayCalories}</p>

                <div className='flex-box'>
                    <Pie options={options} data={nutritionDataDay} />
                    <Bar options={options} data={nutritionDataDay} />
                </div>
            </div>

        </div >



    )
}