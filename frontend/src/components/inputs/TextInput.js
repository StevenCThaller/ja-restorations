import React from 'react'
import { inputLabel } from './Inputs.module.css';
const TextInput = props => {
    const { name, label, value, onChange, autoComplete } = props;
    
    return (
        <div className="formGroup">
            <label className={inputLabel} htmlFor={name}>{label}</label>
            <input type="text" name={name} value={value} onChange={onChange} autoComplete={ autoComplete ? autoComplete : "off" }/>
        </div>
    )
}

export default TextInput
