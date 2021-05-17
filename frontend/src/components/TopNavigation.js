import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import { withRouter, NavLink } from 'react-router-dom';
import { decryptEmail } from '../actions/userActions';
const login = state => {

}


const TopNavigation = props => {
    const { auth, user } = props;
    let loginText = "Log In";
    let loginLink = "/login";
    if(auth.isAuthenticated){
        loginText = "Log Out";
        loginLink = "/logout";
    }

    return (
        <>
            <nav>
                <div>
                    <NavLink exact to="/">JA Restorations</NavLink>
                    <p>Welcome, { auth.user }</p>
                </div>
                <div>
                    <ul>
                        <li><NavLink exact to="/">Home</NavLink></li>
                        <li><NavLink exact to={loginLink}>{loginText}</NavLink></li>
                    </ul>
                </div>
            </nav>
        </>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth,
        user: state.user
    };
}


export default withRouter(connect(mapStateToProps)(TopNavigation));