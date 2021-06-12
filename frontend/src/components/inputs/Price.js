import React, { useState, useEffect } from 'react'
import { inputError, errorMessage, inline, formGroup, hasError, inputLabel } from './Inputs.module.css';
import BoxSlider from './BoxSlider';
const Price = props => {
    const { className, canBeRange, isRange, floorName, ceilingName, label, floorValue, ceilingValue, onChange, floorError, ceilingError } = props;
    const [focus, setFocus] = useState('');
    const [display, setDisplay] = useState({
        priceFloor: floorValue === '' ? '' : `$${floorValue.toFixed(2)}`,
        priceCeiling: ceilingValue === '' ? '' : `$${ceilingValue.toFixed(2)}`
    })
    const [isPriceRange, setIsPriceRange] = useState( isRange === null ? false : isRange);

    const togglePriceRange = () => {
        if(isPriceRange){
            onChange({ target: { name: 'priceCeiling', value: '' } })
        } else {
            onChange({ target: { name: 'priceCeiling', value: 0 } });
        }
        setIsPriceRange(r => !r);
    }

    useEffect(() => {

        setDisplay({
            ...display,
            priceCeiling: ceilingValue === '' ? '' : `$${ceilingValue.toFixed(2)}`
        })
    }, [isPriceRange])

    const changeHandler = e => {
        let { name, value } = e.target;

        if(value[value.length-1] !== '.' && isNaN(parseInt(value))){
            setDisplay(0);
            onChange({target: { name, value: 0 }});
            return;
        } else if((value[value.length-1] === '.' && display[name].toString().includes('.') && value.length > display[name].length) || value.toString()[value.length-4] === '.'){
            return;
        }

        if(value[0] === "0"){
            value = parseFloat(value, 10);
        }
        
        setDisplay({
            ...display,
            [name]: isNaN(value) ? '' : value
        });

        onChange(e);
    }
   
    const stripPrice = e => {
        const { name, value } = e.target;
        setFocus(name);
        let newVal = parseFloat(value.slice());
        setDisplay({
            ...display,
            [name]: isNaN(newVal) ? '' : parseFloat(value.slice(1))
        });
    }

    const formatPrice = e => {
        const { value } = e.target;
        let newVal = parseFloat(value.slice());
        setDisplay({
            ...display,
            [focus]: isNaN(newVal) ? `$${parseFloat('0').toFixed(2)}` : `$${parseFloat(value).toFixed(2)}`
        });
        setFocus('');
    }

    return (
        <div className={`input-range ${className}`}>
            {
                floorError ? 
                <span className={errorMessage}>{floorError}</span>
                :
                ''
            }
            <div className={`w100 flex-between`}>
                <label htmlFor={floorName}>{label}</label>
                {
                    canBeRange ?
                    <BoxSlider
                        className={inline}
                        name="isPriceRange"
                        optionOne="Fixed"
                        optionTwo="Range"
                        value={isPriceRange}
                        onChange={togglePriceRange}
                    />
                    :
                    ''
                }
            </div>
            {
                isPriceRange ?
                <div className="w100 flex-between">
                    <input 
                        className={ floorError ? inputError : '' } 
                        type="currency" 
                        onFocus={stripPrice} 
                        onBlur={formatPrice} 
                        name={floorName} 
                        onChange={changeHandler} 
                        value={display.priceFloor} 
                        autoComplete="off" 
                        />
                    <label htmlFor={ceilingName}>&nbsp;&#8212;&nbsp;</label>
                    <input 
                        className={ ceilingError ? inputError : '' } 
                        type="currency" 
                        onFocus={stripPrice} 
                        onBlur={formatPrice} 
                        name={ceilingName} 
                        onChange={changeHandler} 
                        value={display.priceCeiling} 
                        autoComplete="off" 
                    />
                </div>
                : 
                <input 
                    className={ floorError ? inputError : '' } 
                    type="currency" 
                    onFocus={stripPrice} 
                    onBlur={formatPrice} 
                    name={floorName} 
                    onChange={changeHandler} 
                    value={display.priceFloor} 
                    autoComplete="off" 
                />
            }
        </div>
    )
}

export default Price
