import React, { useEffect } from 'react'
import { GoogleLogin } from 'react-google-login';
import { connect } from 'react-redux';
import { login } from '../actions/authActions';
import { setUser } from '../actions/userActions';
import config from '../config.json';
import { withRouter, Redirect } from 'react-router-dom';
import axios from 'axios';
import jwt from 'jsonwebtoken';
import Modal from 'react-bootstrap/Modal';

const Login = props => {
    const { login, auth, user, setUser } = props;

    const onFailure = error => {
        alert(error);
    }

    const googleResponse = response => {
        if(!response.tokenId) {
            console.error("Unable to get tokenId from Google", response);
            return;
        }

        const tokenBlob = new Blob([JSON.stringify({ tokenId: response.tokenId }, null, 2)], { type: 'application/json' });
        
        const options = {
            method: 'POST',
            body: tokenBlob,
            mode: 'cors',
            cache: 'default'
        };

        fetch(config.GOOGLE_AUTH_CALLBACK_URL, options)
            .then(response => response.json())
            .then(user => {
                const token = user.token;
                login(token);
                return { uId: jwt.decode(token).UserId, token };
            })
            .then(({uId, token}) => axios.get(`http://localhost:5000/api/users/${uId}`, { headers: { Authorization: `Bearer ${token}` } }))
            .then(user => setUser(user.data.value.results))
            .catch(err => console.log(err));
    }

    let content = !!auth.isAuthenticated ?
        (
            <Redirect to={{pathname: "/" }}/>
        ) :
        (
            <p>oops</p>
        )

    return (
        <>
            { content }
        </>
    )
}
const mapStateToProps = state => {
    return {
        auth: state.auth,
        user: state.user
    };
};

const mapDispatchToProps = dispatch => {
    return {
        login: token => {
            dispatch(login(token));
        },
        setUser: token => {
            dispatch(setUser(token))
        }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Login));
