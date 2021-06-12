import React from 'react'
import { inputLabel } from './Inputs.module.css';

const DateField = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label className={inputLabel} htmlFor={name}>{label}</label>
            <input type="date" name={name} value={value} onChange={onChange}/>
        </div>
    )
}

export default DateField
