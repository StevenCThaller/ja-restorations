import React, { useEffect, useState } from 'react'
import ImagesInput from '../components/ImagesInput';
import axios from 'axios';
import DataList from '../components/inputs/DataList';
import { textHandler, numberHandler, resetFurniture } from '../actions/furnitureActions';
import { withRouter } from 'react-router';
import { connect } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { getFurnitureTypes, convertFilesToFormData, submitFurniture, submitImages } from '../services/furnitureService';
import { GetHeaders } from '../services/authService';
import TextInput from '../components/inputs/TextInput';
import TextArea from '../components/inputs/TextArea';
import BoxSlider from '../components/inputs/BoxSlider';
import Price from '../components/inputs/Price';

const initialErrors = {
    name: '',
    description: '',
    type: '',
    priceFloor: '',
    priceCeiling: '',
    length: '',
    width: '',
    height: '',
    weight: ''
}

const FurnitureForm = props => {
    const { auth, furniture, textChangeHandler, numberChangeHandler, resetFurniture } = props;
    const [errors, setErrors] = useState(initialErrors)
    const [images, setImages] = useState({});
    const [types, setTypes] = useState([]);
    const [displayImages, setDisplayImages] = useState([]);
    const [isPriceRange, setIsPriceRange] = useState(false);
    const history = useHistory();

    useEffect(() => {
        getFurnitureTypes()
            .then(response => {
                setTypes(response.data.types)
            })
            .catch(err => console.log(err));
    }, []);


    const addImages = e => {
        setImages(e.target.files);
        setDisplayImages([...e.target.files]);
    }

    const removeImage = i => {
        setDisplayImages([...displayImages.slice(0, i), ...displayImages.slice(i+1)])
    }

    const submitHandler = e => {
        e.preventDefault();
        console.log(GetHeaders(auth));
        if(images.hasOwnProperty(0)) {
            submitFurniture(furniture, GetHeaders(auth))
                .then(response => submitImages(response.data.value.results, convertFilesToFormData(images), GetHeaders(auth)))
                .then(() => {
                    resetFurniture();
                    history.push('/')
                })
                .catch(err => {
                    let { ...newErrors } = initialErrors;

                    for(const error in err.response.data.errors){
                        if(newErrors.hasOwnProperty(error)){
                            newErrors[error] = err.response.data.errors[error]
                        }
                    }
                    if(newErrors != initialErrors){
                        setErrors(newErrors);
                    }
                });
        } else {
            submitFurniture(furniture, GetHeaders(auth))
                .then(() => {
                    resetFurniture();
                    history.push('/')
                })
                .catch(err =>{
                    let { ...newErrors } = initialErrors;

                    for(const error in err.response.data.errors){
                        console.log(error);
                        if(newErrors.hasOwnProperty(error)){
                            newErrors[error] = err.response.data.errors[error]
                        }
                    }
                    if(newErrors != initialErrors){
                        setErrors(newErrors);
                    }
                });
            
        }
    }

    const toTitleCase = st => st.split(' ').map( word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');


    return (
        <form onSubmit={submitHandler}>
            <TextInput 
                name="name" 
                label="Name: " 
                value={furniture.name} 
                onChange={textChangeHandler}
            />
            <TextArea 
                name="description" 
                label="Description: " 
                value={furniture.description} 
                onChange={textChangeHandler}
            />
            <DataList 
                id="types" 
                name="type" 
                label="Type: "
                onChange={ textChangeHandler } 
            >
                {types.map((ty, i) => <option key={i} value={ toTitleCase(ty.name)}/>)}
            </DataList>
            <BoxSlider
                name="isPriceRange"
                optionOne="Fixed"
                optionTwo="Range"
                value={isPriceRange}
                onChange={() => setIsPriceRange(r => !r)}
            />
            <div className="formGroup" style={{display: 'flex'}}>
                <Price
                    additionalClasses='w40'
                    name="priceFloor"
                    label="Price: "
                    value={furniture.priceFloor}
                    onChange={numberChangeHandler}
                    error={errors.priceFloor}
                />
                {
                    isPriceRange ?
                    <Price
                        additionalClasses='w40'
                        name="priceCeiling"
                        label="&nbsp;&nbsp;--&nbsp;&nbsp;"
                        value={furniture.priceCeiling}
                        onChange={numberChangeHandler}
                        error={errors.priceCeiling}
                    />
                    :
                    ''
                }
            </div>
            {/* <div>
                <label htmlFor="priceFloor">{ !isPriceRange ? "Price: " : "Minimum Price" }</label>
                <input type="number" name="priceFloor" onChange={numberChangeHandler} />
            </div> */}
            <div>
                <label htmlFor="dimensions">Dimensions (LxWxH in): </label>
                <input type="number" name="length" onChange={numberChangeHandler}/>
                <span> x </span>
                <input type="number" name="height" onChange={numberChangeHandler}/>
                <span> x </span>
                <input type="number" name="width" onChange={numberChangeHandler}/>
            </div>
            <div>
                <label htmlFor="estimatedWeight">Estimated Weight (lbs): </label>
                <input type="number" name="estimatedWeight" onChange={numberChangeHandler} />
            </div>
            
            <ImagesInput images={displayImages} addImages={addImages} removeImage={removeImage} multiple={true} />
        
            <input type="submit" value="Submit" />
        </form>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        images: state.images,
        types: state.types,
        displayImages: state.displayImages,
        isPriceRange: state.isPriceRange,
        furniture: state.furn
    }
}

const mapDispatchToProps = dispatch => {
    return {
        textChangeHandler: target => {
            dispatch(textHandler(target))
        },
        numberChangeHandler: target => {
            dispatch(numberHandler(target))
        },
        resetFurniture: () => {
            dispatch(resetFurniture())
        }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(FurnitureForm));
