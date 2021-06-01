import { useState } from 'react'
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { navCat } from './TopNavigation/TopNavigation.module.css';
import Modal from 'react-bootstrap/Modal';
import { GoogleLogin } from 'react-google-login';
import config from '../config.json';
import { login } from '../actions/authActions';
import jwt from 'jsonwebtoken';
import { setUser } from '../actions/userActions';
import axios from 'axios';
import NavUser from './NavUser';

const UserStatus = props => {
    const { auth, user, login, setUser } = props;
    const [show, setShow] = useState(false);
    

    const handleClose = () => setShow(false);
    const handleOpen = () => setShow(true);

    // let loginText = "Sign In";
    // // let loginLink = "/login";
    // if(auth.isAuthenticated){
    //     loginText = user.email;
    //     // loginLink = "/logout";
    // }

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
                handleClose();
                return { uId: jwt.decode(token).UserId, token };
            })
            .then(({uId, token}) => axios.get(`http://localhost:5000/api/users/${uId}`, { headers: { Authorization: `Bearer ${token}` } }))
            .then(user => setUser(user.data.value.results))
            .catch(err => console.log(err));
    }

    let content = !!auth.isAuthenticated ?
        (
            <NavUser 
                email={user.email}
                role={user.Role}
                navCat={navCat}
            />
        ) :
        (
            <span className={navCat} onClick={handleOpen}>
                Sign In
            </span>
        )

    return (
        <>
            {content}
            <Modal className="my-modal" show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Log In</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <p>Please log in with one of the following services.</p>
                        <GoogleLogin 
                            className="yellow-button"
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
                    </Modal.Body>
            </Modal>
        </>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth,
        user: state.user
    };
}

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


export default withRouter(connect(mapStateToProps, mapDispatchToProps)(UserStatus));