import React, { useState, useRef } from 'react'
import PropTypes from 'prop-types'


import { imagesInput } from './ImagesInput.module.css';
import ImageForUpload from './ImageForUpload';

const ImagesInput = props => {
    const { images, addImages, removeImage, multiple } = props;
    const fileInput = useRef();

    const remove = i => {
        removeImage(i);
    }
    
    // const addImagesToForm = e => {
    //     e.preventDefault();
    //     addImages(e);


    // }


    return (
        <div>
        
            <button type="button">
                <label htmlFor="images">Select images to upload:</label>
            </button>
            <input type="file" id="images" style={{visibility: 'hidden'}} ref={fileInput} multiple={multiple ? true : false} onChange={addImages} accept="image/*"/>
            <div className="selected-images">

            {
                images.map((img, i) => <ImageForUpload key={i} i={i} remove={remove} src={URL.createObjectURL(img)}/>)
            }
            </div>
        
        </div>
    )
}

export default ImagesInput






