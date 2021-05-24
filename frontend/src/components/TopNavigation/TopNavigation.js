import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import { withRouter, NavLink } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import NavLogo from '../../containers/NavLogo/NavLogo';
const login = state => {

}


const TopNavigation = props => {
    const { auth } = props;
    const history = useHistory();

    let loginText = "Log In";
    let loginLink = "/login";
    if(auth.isAuthenticated){
        loginText = "Log Out";
        loginLink = "/logout";
    }

    return (
        <>
            <nav>
                <NavLogo/>
                
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
