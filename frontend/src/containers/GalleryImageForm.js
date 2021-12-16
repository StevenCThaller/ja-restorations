import React, { useState } from 'react'
import { submitGalleryImage, convertFilesToFormData } from '../services/imageService';
import ImagesInput from '../components/ImagesInput';
import { GetHeaders } from '../services/authService';
import { useHistory } from 'react-router-dom';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { resetImages, resetDisplayImages } from '../actions/galleryActions';

const initialErrors = {

}

const GalleryImageForm = props => {
    const { auth, resetImages, resetDisplayImages } = props;

    const [errors, setErrors] = useState(initialErrors);
    const [images, setImages] = useState({});
    const [displayImages, setDisplayImages] = useState([]);
    const history = useHistory();

    const addImages = e => {
        setImages(e.target.files);
        setDisplayImages([...e.target.files]);
    }

    const removeImage = i => {
        setDisplayImages([...displayImages.slice(0, i), ...displayImages.slice(i+1)])
    }

    const submitHandler = e => {
        e.preventDefault();
        if(images.hasOwnProperty(0)) {
            submitGalleryImage(convertFilesToFormData(images), GetHeaders(auth))
                .then(res => {
                    console.log(res)
                    resetImages();
                    history.push('/')
                })
                .catch(err => {
                    if(err['response'] !== undefined){

                        let { ...newErrors } = initialErrors;
                        
                        for(const error in err.response.data.errors){
                            if(newErrors.hasOwnProperty(error)){
                                newErrors[error] = err.response.data.errors[error]
                            }
                        }
                        if(newErrors != initialErrors){
                            setErrors(newErrors);
                        }
                    }
                });
        } 
    }
    

    return (
        <form onSubmit={submitHandler}>
            <ImagesInput images={displayImages} addImages={addImages} removeImage={removeImage} multiple={true} />
        
            <input type="submit" value="Submit" />
        </form>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        images: state.images, 
        displayImages: state.displayImages
    }
}

const mapDispatchToProps = dispatch => {
    return {
        resetImages: () => {
            dispatch(resetImages())
        }, 
        resetDisplayImages: () => {
            dispatch(resetDisplayImages())
        }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(GalleryImageForm));
