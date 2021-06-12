import axios from 'axios';
import React, { useEffect, useState } from 'react'
import FurnitureList from '../FurnitureList/FurnitureList';

const Gallery = () => {
    const [furnitureList, setFurnitureList] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/furniture')
            .then(response => {
                setFurnitureList(response.data.value.results)
            })
            .catch(err => console.log(err));
        return () => setFurnitureList([]);
    }, [])

    const deleteFromDom = id => {
        setFurnitureList(furnitureList.filter(f => f.furnitureId !== id));
    }

    const sellFurniture = (id, sale) => {
        let i = furnitureList.indexOf(furnitureList.find(f => f.furnitureId === id));
        setFurnitureList([...furnitureList.slice(0, i), { ...furnitureList[i], sale, sold: true } , ...furnitureList.slice(i+1)])
    }

    return (
        <FurnitureList 
            furnitureList={furnitureList}
            setFurnitureList={setFurnitureList}
            deleteFurniture={deleteFromDom}
            sellFurniture={sellFurniture}
        />
    )
}

export default Gallery
