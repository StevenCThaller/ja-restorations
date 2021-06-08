import React from 'react'

const DataList = props => {
    const { id, name, label, children, changeHandler } = props;

    return (
        <div>
            <label htmlFor={name}>{label}</label>
            <input list={id} name={name} onChange={ changeHandler } autoComplete="off"/>
            <datalist id={id}>
                { children }
            </datalist>
        </div>
    )
}

export default DataList
