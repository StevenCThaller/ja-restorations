import React from 'react'


const ImageForUpload = props => {
    const { i, src, remove } = props;

    return (
        <div className="image-upload-container">
            <button className="remove-from-upload" type="button" onClick={() => remove(i) }>&#10005;</button>
            <img className="image-upload" src={src}/>
        </div>
    )
}

export default ImageForUpload
