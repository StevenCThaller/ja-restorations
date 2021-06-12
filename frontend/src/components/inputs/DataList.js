import React, { useState } from 'react'
import { inputLabel } from './Inputs.module.css'

const DataList = props => {
    const { id, name, label, values, onChange } = props;
    const toTitleCase = st => st.split(' ').map( word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');
    const [newThing, setNewThing] = useState('');
    return (
        <div>
            <label className={inputLabel} htmlFor={name}>{label}</label>
            <input list={id} name={name} onChange={ onChange } autoComplete="off"/>
            <datalist id={id}>
                {values.map((val, i) => <option key={i} value={ toTitleCase(val) }/>)}
            </datalist>
        </div>
    )
}

export default DataList
