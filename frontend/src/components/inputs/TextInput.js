import React from 'react'

const TextInput = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label htmlFor={name}>{label}</label>
            <input type="text" name={name} value={value} onChange={onChange}/>
        </div>
    )
}

export default TextInput
