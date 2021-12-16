import React, { useState, useEffect } from 'react'
import axios from 'axios';
import Carousel from 'react-bootstrap/Carousel';
import { headerCarousel, carouselImage } from './MainHeader.module.css';

const MainHeader = () => {
    const [images, setImages] = useState([]);
    const [loaded, setLoaded] = useState(false);
    const [index, setIndex] = useState(0);

    useEffect(() => {
        axios.get('http://localhost:5000/api/images/gallery/3')
            .then(response => {
                // console.log(response);
                setImages(response.data.value.results.filter(i => i !== null));
                setLoaded(true);
            })
            .catch(err => alert(err));
    }, [])

    const handleSelect = (selectedIndex, e) => {
        setIndex(selectedIndex);
    }

    if(!loaded) {
        return <p>Loading...</p>
    } else if(images.length === 0) {
        return <></>;
    }

    return (
        <Carousel className={headerCarousel} activeIndex={index} onSelect={handleSelect} interval={3000}>
            {
                images.map((image, i) => 
                    // image !== null ?? 
                    <Carousel.Item key={i}>
                        <img className={`d-block ${carouselImage} `} src={image.url} alt={`Furniture image # ${i}`} />
        
                    {/* <p className="legend">{legend}</p> */}
                    </Carousel.Item>
                )
            }
        </Carousel>
    )
}

export default MainHeader
