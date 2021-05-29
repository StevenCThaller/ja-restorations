import React, { useState, useEffect } from 'react'
import { withRouter } from 'react-router';
import { NavLink } from 'react-router-dom';
import { connect } from 'react-redux';
import axios from 'axios';
import jwt from 'jsonwebtoken';
import MainHeader from '../containers/MainHeader/MainHeader';
import DeleteFurniture from '../containers/DeleteFurniture';

const Home = props => {
    const { user } = props;
    const [furniture, setFurniture] = useState([]);

    console.log(user);

    useEffect(() => {
        let token = jwt.decode(props.auth.user);
        console.log(token);
        axios.get('http://localhost:5000/api/furniture', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        }
        )
            .then(response => setFurniture(response.data.data))
            .catch(err => console.log(err));
    }, [])


    return (
        <div>
            <NavLink to="/addfurniture">Furniture</NavLink>
            <MainHeader/>
            
        </div>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth,
        user: state.user
    }
}


export default withRouter(connect(mapStateToProps)(Home));
