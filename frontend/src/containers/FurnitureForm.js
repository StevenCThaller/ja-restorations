import React, { useEffect, useState } from 'react'
import ImagesInput from '../components/ImagesInput';
import axios from 'axios';
import DataList from '../components/inputs/DataList';
import { textHandler, numberHandler } from '../actions/furnitureActions';
import { withRouter } from 'react-router';
import { connect } from 'react-redux';

String.prototype.hashUrlCode = function() {
    var hash = 0, i, chr;
    let split = this.split('.');
    if (split[0].length === 0) return hash;
    for (i = 0; i < split[0].length; i++) {
        chr   = split[0].charCodeAt(i);
        hash  = ((hash << 10) - hash) + chr;
        hash |= 0; // Convert to 32bit integer
    }
    return hash+'.'+split[1];
};

const FurnitureForm = props => {
    const { auth, furniture, textChangeHandler, numberChangeHandler } = props;
    const [images, setImages] = useState({});
    const [types, setTypes] = useState([]);
    const [displayImages, setDisplayImages] = useState([]);
    const [isPriceRange, setIsPriceRange] = useState(false);


    useEffect(() => {
        getTypes(setTypes);
    }, []);

    const getTypes = set => {
        axios.get('http://localhost:5000/api/furniture/types')
            .then(response => {
                set(response.data.types)
            })
            .catch(err => console.log(err));
    }


    const addImages = e => {
        setImages(e.target.files);
        setDisplayImages([...e.target.files]);
    }

    const removeImage = i => {
        setDisplayImages([...displayImages.slice(0, i), ...displayImages.slice(i+1)])
    }


    

    const submitHandler = e => {
        e.preventDefault();
        let formData = new FormData();
        for(let i = 0; i < images.length; i++) {
            formData.append('fileNames', images[i].name.hashUrlCode());
            formData.append('formFiles', images[i]);
        }

        if(images.hasOwnProperty(0)) {
            submitFurniture(furniture, { Authorization: `Bearer ${auth.user}`})
                .then(response =>{
                    console.log(response);
                    return submitImages(response.data.value.results, formData, { headers: { Authorization: `Bearer ${auth.user}`} })
                })
                .catch(err => console.log(err));
        } else {
            submitFurniture(furniture, { Authorization: `Bearer ${auth.user}`})
                .catch(err => console.log(err));
            
        }
    }

    const submitFurniture = (body, headers) => axios({ url: 'http://localhost:5000/api/furniture', method: 'POST', data: body, headers })

    const submitImages = (id, formData, headers) => axios.post(`http://localhost:5000/api/images/furniture/${id}`, formData, headers)

    return (
        <form onSubmit={submitHandler}>
            <div>
                <label htmlFor="name">Name: </label>
                <input type="text" name="name" onChange={ textChangeHandler } />
            </div>
            <div>
                <label htmlFor="description">Description: </label>
                <textarea name="description" cols="30" rows="4" onChange={textChangeHandler}></textarea>
            </div>
            <DataList isFor="type" listVal="types" displayVal="name" changeHandler={ textChangeHandler } data={ furniture.type } setData={ numberChangeHandler } values={types}/>
            <div>
                <label htmlFor="priceFloor">{ !isPriceRange ? "Price: " : "Minimum Price" }</label>
                <input type="number" name="priceFloor" onChange={numberChangeHandler} />
                <label htmlFor="isPriceRange">Price Range: </label>
                <input type="checkbox" name="isPriceRange" onChange={ () => setIsPriceRange(!isPriceRange) } checked={isPriceRange}/>
            </div>
            {
                isPriceRange ?
                <div>
                    <label htmlFor="priceCeiling">Maximum Price</label>
                    <input type="number" name="priceCeiling" onChange={numberChangeHandler} />
                </div>
                :
                ''
            }
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
            
            <ImagesInput images={displayImages} addImages={addImages} removeImage={removeImage}  />
        
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
        furniture: state.furnForm
    }
}

const mapDispatchToProps = dispatch => {
    return {
        textChangeHandler: target => {
            dispatch(textHandler(target))
        },
        numberChangeHandler: target => {
            dispatch(numberHandler(target))
        }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(FurnitureForm));
