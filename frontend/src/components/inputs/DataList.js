import React from 'react'

const DataList = props => {
    const { listVal, isFor, values, data, displayVal, changeHandler } = props;

    return (
        <div>
            <label htmlFor={isFor}>Type:</label>
            <input list={listVal} name={isFor} onChange={ changeHandler } />
            <datalist id={listVal}>
                {
                    values.map((val, i) => <option className="proper-case" key={i} value={val[displayVal]}/>)
                }
            </datalist>
        </div>
    )
}

export default DataList
