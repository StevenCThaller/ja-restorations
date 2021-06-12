import React from 'react'
import { inputLabel } from './Inputs.module.css';
const NumberInput = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label className={inputLabel} htmlFor={name}>{label}</label>
            <input type="number" name={name} value={value} onChange={onChange}/>
        </div>
    )
}

export default NumberInput
