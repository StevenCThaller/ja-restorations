import React, { useEffect, useState } from 'react'
import NavLogo from '../../containers/NavLogo/NavLogo';
import NavSearch from '../../containers/NavSearch/NavSearch';
import UserStatus from '../../containers/UserStatus/UserStatus';
import style from './TopNavigation.module.css';
import { NavLink } from 'react-router-dom';

const TopNavigation = () => {
    const { topNav, navCat, navLeft, navRight } = style;
    

    return (
        <nav id={topNav}>
            <div className={navLeft}>
                <NavLogo/>
                <span className={navCat}>Categories</span>
                <NavLink className={navCat} to="/available">Shop</NavLink>
                <NavLink className={navCat} to="/gallery">Gallery</NavLink>
            </div>
            <div className={navRight}>
                <NavSearch />
                <UserStatus className={navCat} />
            </div>
        </nav>
    )
}



export default TopNavigation;
