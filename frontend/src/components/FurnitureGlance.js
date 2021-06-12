import React from 'react'
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import DeleteFurniture from '../containers/DeleteFurniture';
import SellFurniture from '../containers/SellFurniture';
import UnlikeFurniture from '../containers/UnlikeFurniture';
import LikeFurniture from '../containers/UnlikeFurniture';

const FurnitureGlance = props => {
    const { index, furniture, user, removeLike, addLike, deleteFromDom, soldItem } = props;
    const { 
        name, 
        description, 
        length, 
        width, 
        height, 
        priceCeiling, 
        priceFloor, 
        furnitureId, 
        likedByUsers,
        images,
        sale,
        sold
    } = furniture;


    return (
        <div>
            <h4>{name}</h4>
            <p>{description}</p>
            <p>Dimensions (LxWxH): {length} x {width} x {height}</p>
            <p>Status: { sold ? 'Sold' : 'Available' }</p>
            <p>
                {
                    sold ?
                    `Final Price: $${sale.finalPrice}`
                    :
                    `Price ${priceCeiling ? ` range: \$${priceFloor} - \$${priceCeiling}`: `: \$${priceFloor}` } `
                }    
            </p>
            {
                images.map((img, j) => <img className="furniture-image" key={j} src={img.url} />)
            }
            {
                user.role > 1 ?
                <>
                <DeleteFurniture id={furnitureId} deleteFromDom={deleteFromDom}/> 
                {
                    !sold ?
                    <SellFurniture id={furnitureId} soldItem={soldItem} />
                    :
                    ''
                }
                </>
                : 
                likedByUsers.find(u => u.userId == user.userId) ?
                <UnlikeFurniture furnitureLike={likedByUsers.find(l => l.userId == user.userId)} removeLike={() => removeLike(index)}/>
                :
                <LikeFurniture furnitureId={furnitureId} addLike={newLike => addLike(index, newLike)}/>
            }
        </div>
    )
}

const mapStateToProps = state => {
    return {
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(FurnitureGlance))
