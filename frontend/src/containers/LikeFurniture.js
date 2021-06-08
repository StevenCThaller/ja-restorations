import axios from 'axios';
import React from 'react'
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { GetHeaders } from '../services/authService';

const LikeFurniture = props => {
    const { furnitureId, addLike, auth, user } = props;

    const likeFurniture = () => {
        axios.post(`http://localhost:5000/api/furniture/like`, { userId: user.userId, furnitureId }, GetHeaders(auth))
            .then(response => addLike(response.data.value.results))
            .catch(err => console.log(err));
    }

    return (
        <button onClick={ likeFurniture }>Like</button>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(LikeFurniture));