import React from 'react'
import { rSwitch, slider } from './Inputs.module.css';

const BoxSlider = props => {
    const { className, name, optionOne, optionTwo, value, onChange } = props;
    return (
        // <div className="formGroup">
        //     <label htmlFor={name}>{label}</label>
        //     <input type="checkbox" name={name} value={value} onChange={onChange}/>
        // </div>

        <div className={`${className}`}>
            <span>{optionOne} </span>
            <label className={rSwitch}>
                <input type="checkbox" name={name} value={value} onChange={onChange}/>
                <span className={slider}></span>
            </label>
            <span> {optionTwo}</span>
        </div>
    )
}

export default BoxSlider
