import React from 'react'

const DataList = props => {
    const { listVal, isFor, values, data, displayVal, changeHandler } = props;

    const toTitleCase = string => string.split(' ').map( word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');

    return (
        <div>
            <label htmlFor={isFor}>Type:</label>
            <input list={listVal} name={isFor} onChange={ changeHandler } autoComplete="off"/>
            <datalist id={listVal}>
                {
                    values.map((val, i) => <option key={i} value={ toTitleCase(val[displayVal])}/>)
                }
            </datalist>
        </div>
    )
}

export default DataList
