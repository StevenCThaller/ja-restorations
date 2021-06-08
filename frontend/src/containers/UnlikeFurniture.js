import axios from 'axios';
import React from 'react'
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { GetHeaders } from '../services/authService';

const UnlikeFurniture = props => {
    const { furnitureLike, removeLike, auth } = props;

    const unLike = () => {
        axios.delete(`http://localhost:5000/api/furniture/${furnitureLike.furnitureLikeId}/unlike`, GetHeaders(auth))
            .then(response => {
                removeLike();
            })
            .catch(err => console.log(err));
    }
    return (
        <button onClick={unLike}>Unlike</button>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth
    }
}

export default withRouter(connect(mapStateToProps)(UnlikeFurniture));
