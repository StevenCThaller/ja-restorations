import React from 'react'

const TextArea = props => {
    const { name, label, value, onChange } = props;
    return (
        <div className="formGroup">
            <label htmlFor={name}>{label}</label>
            <textarea name={name}  cols="40" rows="3" onChange={onChange} value={value}></textarea>
        </div>
    )
}

export default TextArea
