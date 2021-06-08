import React, { useState } from 'react'
import { inputError, errorMessage, formGroup, hasError } from './Inputs.module.css';

const Price = props => {
    const { name, label, value, onChange, error, additionalClasses } = props;
    const [display, setDisplay] = useState(`$${value.toFixed(2)}`)

    const changeHandler = e => {
        const { name, value } = e.target;

        if(isNaN(parseInt(value))){
            setDisplay(0);
            onChange({target: { name, value: 0 }});
            return;
        }

        setDisplay(value);

        onChange(e);
    }
   
    const stripPrice = e => {
        const { name, value } = e.target;

        setDisplay(parseFloat(value.slice(1)));
    }

    const formatPrice = e => {
        const { name, value } = e.target;
        if(value.length < 1){

        }
        setDisplay(`$${parseFloat(value).toFixed(2)}`)
    }

    return (
        <div className={`${formGroup} ${error ? hasError : ''} ${additionalClasses}`}>
            {
                error ? 
                <span className={errorMessage}>{error}</span>
                :
                ''
            }
            <label htmlFor={name}>{label}</label>
            <input className={ error ? inputError : '' } type="currency" onFocus={stripPrice} onBlur={formatPrice} name={name} onChange={changeHandler} value={display} />
        </div>
    )
}

export default Price
