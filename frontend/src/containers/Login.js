import React, { useEffect } from 'react'
import { GoogleLogin } from 'react-google-login';
import { connect } from 'react-redux';
import { login } from '../actions/authActions';
import { decryptToken } from '../actions/userActions';
import config from '../config.json';
import { withRouter, Redirect } from 'react-router-dom';
import axios from 'axios';

const Login = props => {
    const { login, auth } = props;

    const onFailure = error => {
        alert(error);
    }

    const googleResponse = response => {
        console.log(response)
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
            })
            .catch(err => console.log(err));
    }

    let content = !!auth.isAuthenticated ?
        (
            <div>
                <Redirect to={{pathname: "/" }}/>
            </div>
        ) :
        (
            <div>
                <GoogleLogin 
                    clientId={config.GOOGLE_CLIENT_ID}
                    buttonText="Google Login"
                    onSuccess={googleResponse}
                    onFailure={googleResponse}
                    // isSignedIn={true}
                    fetchBasicProfile={false}
                    // scope="openid"
                    accessType="offline"
                    // responseType="token_id"
                />
            </div>
        )

    return (
        <div>
            <h2>Login</h2>
            { content }
        </div>
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
        // decryptToken: token => {
        //     dispatch(decryptToken(token))
        // }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Login));
