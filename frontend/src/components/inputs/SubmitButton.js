import React from 'react'

const SubmitButton = props => {
    const { text } = props;
    return (
        <div className="formGroup">
            <input type="submit" value={text}/>
        </div>
    )
}

export default SubmitButton
