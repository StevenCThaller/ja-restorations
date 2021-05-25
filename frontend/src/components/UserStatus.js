import React from 'react'
import { connect } from 'react-redux';
import { NavLink, withRouter } from 'react-router-dom';
import { navCat } from './TopNavigation/TopNavigation.module.css';

const UserStatus = props => {
    const { auth } = props;

    let loginText = "Sign In";
    let loginLink = "/login";
    if(auth.isAuthenticated){
        loginText = "username";
        loginLink = "/logout";
    }
    return (
        <span className={navCat}>
            <NavLink exact to={loginLink}>{loginText}</NavLink>
        </span>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth,
        user: state.user
    };
}


export default withRouter(connect(mapStateToProps)(UserStatus));