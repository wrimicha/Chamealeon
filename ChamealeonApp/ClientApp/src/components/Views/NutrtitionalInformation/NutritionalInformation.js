import React from 'react';
import {
    Chart as ChartJS, CategoryScale,
    LinearScale,
    BarElement, ArcElement, Tooltip, Legend
} from 'chart.js';
import { Pie, Bar } from 'react-chartjs-2';
import './NutritionalInformation.css'
import { DropdownButton, Dropdown } from 'react-bootstrap';

// Reference: https://www.chartjs.org/docs/latest/charts/doughnut.html
//https://react-chartjs-2.netlify.app/examples/pie-chart
//https://react-chartjs-2.netlify.app/examples/vertical-bar-chart
//https://react-bootstrap.github.io/components/dropdowns/
ChartJS.register(CategoryScale,
    LinearScale,
    BarElement,
    ArcElement, Tooltip, Legend);
export default function NutritionalInformation() {
    const userName = "Bob Jones";
    const options = {
        responsive: true,
        // maintainAspectRatio: false
    };
    //state
    const day = "Sunday";
    const apiData = [300, 120, 50, 10, 5];
    const nutritionData = {
        labels: ['Carbohydrates', 'Protein', 'Fat', 'Sodium', 'Sugar'],
        datasets: [
            {
                //TODO: from API call
                label: userName,
                data: apiData,
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

    return (

        <div>
            <div>
                <p>Total Calories: {apiData[0]}</p>
                <div className='flex-box'>
                    <Pie options={options} data={nutritionData} />
                    <Bar options={options} data={nutritionData} />
                </div>
            </div>
            <div>
                <DropdownButton title="Select a day">
                    <Dropdown.Item>Sunday</Dropdown.Item>
                    <Dropdown.Item>Monday</Dropdown.Item>
                    <Dropdown.Item>Tuesday</Dropdown.Item>
                    <Dropdown.Item>Wednesday</Dropdown.Item>
                    <Dropdown.Item>Thursday</Dropdown.Item>
                    <Dropdown.Item>Friday</Dropdown.Item>
                    <Dropdown.Item>Saturday</Dropdown.Item>
                </DropdownButton>

                <p>Total Calories for {day}: {apiData[0]}</p>

                <div className='flex-box'>
                    <Pie options={options} data={nutritionData} />
                    <Bar options={options} data={nutritionData} />
                </div>
            </div>

        </div >



    )
}