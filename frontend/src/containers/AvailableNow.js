import { useState, useEffect } from 'react';
import axios from 'axios';
import DeleteFurniture from './DeleteFurniture';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

const AvailableNow = props => {
    const { user } = props;
    const [furnitureList, setFurnitureList] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/furniture/available')
            .then(response => setFurnitureList(response.data.results))
            .catch(err => alert("Something went wrong, please refresh and try again."));
    }, [])

    
    const deleteFromDom = id => {
        setFurnitureList(furnitureList.filter(f => f.furnitureId !== id));
    }
    

    return (
        <>
            {
                furnitureList.map((item, i) => 
                    <div key={i}>
                        <h4>{item.name}</h4>
                        <p>{item.description}</p>
                        <p>Dimensions (LxWxH): {item.length} x {item.width} x {item.height}</p>
                        <p>Price{item.priceCeiling ? ` range: \$${item.priceFloor} - \$${item.priceCeiling}`: `: \$${item.priceFloor}` } </p>
                        {
                            item.images.map((img, j) => <img className="furniture-image" key={j} src={img.url} />)
                        }
                        {
                            user.role > 1 ?
                            <DeleteFurniture id={item.furnitureId} deleteFromDom={deleteFromDom}/> : ''
                        }
                        
                    </div>
                )
            }
        </>
    )
}

const mapStateToProps = state => {
    return { 
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(AvailableNow));
