import React from 'react'

const DateField = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label htmlFor={name}>{label}</label>
            <input type="date" name={name} value={value} onChange={onChange}/>
        </div>
    )
}

export default DateField
