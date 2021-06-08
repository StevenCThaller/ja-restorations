import { useState, useEffect } from 'react';
import axios from 'axios';
import DeleteFurniture from './DeleteFurniture';
import SellFurniture from './SellFurniture';
import LikeFurniture from './LikeFurniture';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import UnlikeFurniture from './UnlikeFurniture';
import FurnitureList from './FurnitureList/FurnitureList';

const AvailableNow = props => {
    const { user } = props;
    const [furnitureList, setFurnitureList] = useState([]);
    useEffect(() => {
        axios.get('http://localhost:5000/api/furniture/available')
            .then(response => setFurnitureList(response.data.results))
            .catch(err => alert("Something went wrong, please refresh and try again."));
    }, [])

    
    // const deleteFromDom = id => {
    //     setFurnitureList(furnitureList.filter(f => f.furnitureId !== id));
    // }

    // const addLike = (i, newLike) => {
    //     let [...newList] = furnitureList;
    //     newList[i].likedByUsers.push(newLike);
    //     setFurnitureList(newList);
    // }

    // const removeLike = i => {
    //     // let [...newList] = furnitureList;
    //     // newList[i].likedByUsers.filter(l => l.userId !== user.userId);
    //     setFurnitureList([...furnitureList.slice(0, i), {...furnitureList[i], likedByUsers: furnitureList[i].likedByUsers.filter(l => l.userId !== user.userId)}, ...furnitureList.slice(i+1, furnitureList.length)]);
    // }

    // return (
    //     <>
    //         {
    //             furnitureList.map((item, i) => 
    //                 <div key={i}>
    //                     <h4>{item.name}</h4>
    //                     <p>{item.description}</p>
    //                     <p>Dimensions (LxWxH): {item.length} x {item.width} x {item.height}</p>
    //                     <p>Price{item.priceCeiling ? ` range: \$${item.priceFloor} - \$${item.priceCeiling}`: `: \$${item.priceFloor}` } </p>
    //                     {
    //                         item.images.map((img, j) => <img className="furniture-image" key={j} src={img.url} />)
    //                     }
    //                     {
    //                         user.role > 1 ?
    //                         <>
    //                         <DeleteFurniture id={item.furnitureId} deleteFromDom={deleteFromDom}/> 
    //                         <SellFurniture id={item.furnitureId} deleteFromDom={deleteFromDom} />
    //                         </>
    //                         : 
    //                         item.likedByUsers.find(u => u.userId == user.userId) ?
    //                         <UnlikeFurniture furnitureLike={item.likedByUsers.find(l => l.userId == user.userId)} removeLike={() => removeLike(i)}/>
    //                         :
    //                         <LikeFurniture furnitureId={item.furnitureId} addLike={newLike => addLike(i, newLike)}/>
    //                     }
    //                 </div>
    //             )
    //         }
    //     </>
    // )

    return (
        <FurnitureList 
            furnitureList={furnitureList}
            setFurnitureList={setFurnitureList}   
        />
    )
}

const mapStateToProps = state => {
    return { 
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(AvailableNow));
