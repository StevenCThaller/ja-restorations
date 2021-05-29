import React from 'react'
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

const DeleteFurniture = props => {
    const { id, deleteFromDom, auth } = props;

    console.log(auth);

    const deleteFurniture = () => {
        axios.delete(`http://localhost:5000/api/furniture/${id}/delete`, { headers: { Authorization: `Bearer ${auth.user}` } })
        .then(results => deleteFromDom(id))
        .catch(err => console.log(err));
    }

    return (
        <button onClick={ deleteFurniture }>Delete</button>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        user: state.user
    }
}


export default withRouter(connect(mapStateToProps)(DeleteFurniture));
