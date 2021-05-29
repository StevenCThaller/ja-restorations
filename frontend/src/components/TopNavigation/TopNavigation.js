import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import { withRouter, NavLink } from 'react-router-dom';
import NavLogo from '../../containers/NavLogo/NavLogo';
import NavSearch from '../../containers/NavSearch/NavSearch';
import UserStatus from '../UserStatus';
import style from './TopNavigation.module.css';
const login = state => {

}


const TopNavigation = props => {
    const { topNav, navCat, navLeft, navRight } = style;
    

    return (
        <nav id={topNav}>
            <div class={navLeft}>
                <NavLogo/>
                <span className={navCat}>Categories</span>
                <NavLink className={navCat} to="/available">Available Now</NavLink>
                <span className={navCat}>Gallery</span>
            </div>
            <div className={navRight}>
                <NavSearch />
                <UserStatus className={navCat} />
            </div>
        </nav>
    )
}

const mapStateToProps = state => {
    return { 
        auth: state.auth,
        user: state.user
    };
}


export default withRouter(connect(mapStateToProps)(TopNavigation));
