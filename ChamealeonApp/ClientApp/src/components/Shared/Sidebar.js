import React from 'react'


export default function Sidebar({ icon, text }) {
    return (
        <div>
            <ul>
                <li>
                    <div>
                        {icon}
                        <span>{text}</span>
                    </div>
                </li>
            </ul>
        </div>
    )
}
