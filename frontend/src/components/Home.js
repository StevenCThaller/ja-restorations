import React, { useState, useEffect } from 'react'
import { withRouter } from 'react-router';
import { connect } from 'react-redux';
import axios from 'axios';

const Home = () => {
    const [furniture, setFurniture] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/furniture/all')
            .then(response => setFurniture(response.data.data))
            .catch(err => console.log(err));
    }, [])

    

    return (
        <div>
            {
                furniture.map((item, i) => 
                    <div key={i}>
                        <h4>{item.name}</h4>
                        <p>{item.description}</p>
                        <p>Dimensions (LxWxH): {item.length} x {item.width} x {item.height}</p>
                        <p>Price{item.priceCeiling ? ` range: \$${item.priceFloor} - \$${item.priceCeiling}`: `: \$${item.priceFloor}` } </p>
                        {
                            item.images.map((img, j) => <img className="furniture-image" key={j} src={img.url} />)
                        }
                    </div>
                )
            }
        </div>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth
    }
}


export default withRouter(connect(mapStateToProps)(Home));
