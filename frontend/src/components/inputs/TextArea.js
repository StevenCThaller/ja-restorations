import React from 'react'
import { inputLabel } from './Inputs.module.css';

const TextArea = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label className={inputLabel} htmlFor={name}>{label}</label>
            <textarea name={name}  cols="40" rows="3" onChange={onChange} value={value}></textarea>
        </div>
    )
}

export default TextArea
