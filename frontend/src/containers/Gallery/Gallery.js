import axios from 'axios';
import React, { useEffect, useState } from 'react'
import FurnitureList from '../FurnitureList/FurnitureList';

const Gallery = props => {
    const [furnitureList, setFurnitureList] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/furniture')
            .then(response => {
                setFurnitureList(response.data.value.results)
            })
            .catch(err => console.log(err));
        return () => setFurnitureList([]);
    }, [])
    return (
        <FurnitureList 
            furnitureList={furnitureList}
            setFurnitureList={setFurnitureList}
        />
    )
}

export default Gallery
