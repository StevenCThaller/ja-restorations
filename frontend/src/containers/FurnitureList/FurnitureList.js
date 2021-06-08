import React from 'react'
import DeleteFurniture from '../DeleteFurniture';
import SellFurniture from '../SellFurniture';
import UnlikeFurniture from '../UnlikeFurniture';
import LikeFurniture from '../LikeFurniture';
import { withRouter } from 'react-router';
import { connect } from 'react-redux';
import { fList } from './FurnitureList.module.css';

const FurnitureList = props => {
    const { user, furnitureList, setFurnitureList } = props;
    const deleteFromDom = id => {
        setFurnitureList(furnitureList.filter(f => f.furnitureId !== id));
    }

    const addLike = (i, newLike) => {
        let [...newList] = furnitureList;
        newList[i].likedByUsers.push(newLike);
        setFurnitureList(newList);
    }

    const removeLike = i => {
        // let [...newList] = furnitureList;
        // newList[i].likedByUsers.filter(l => l.userId !== user.userId);
        setFurnitureList([...furnitureList.slice(0, i), {...furnitureList[i], likedByUsers: furnitureList[i].likedByUsers.filter(l => l.userId !== user.userId)}, ...furnitureList.slice(i+1, furnitureList.length)]);
    }

    return (
        <main className={fList}>
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
                            <>
                            <DeleteFurniture id={item.furnitureId} deleteFromDom={deleteFromDom}/> 
                            <SellFurniture id={item.furnitureId} deleteFromDom={deleteFromDom} />
                            </>
                            : 
                            item.likedByUsers.find(u => u.userId == user.userId) ?
                            <UnlikeFurniture furnitureLike={item.likedByUsers.find(l => l.userId == user.userId)} removeLike={() => removeLike(i)}/>
                            :
                            <LikeFurniture furnitureId={item.furnitureId} addLike={newLike => addLike(i, newLike)}/>
                        }
                    </div>
                )
            }
        </main>
    )
}

const mapStateToProps = state => {
    return {
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(FurnitureList))
