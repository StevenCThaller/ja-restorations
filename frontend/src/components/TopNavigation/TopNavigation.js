import React, { useEffect, useState } from 'react'
import NavLogo from '../../containers/NavLogo/NavLogo';
import NavSearch from '../../containers/NavSearch/NavSearch';
import UserStatus from '../UserStatus';
import style from './TopNavigation.module.css';
import { NavLink } from 'react-router-dom';

const TopNavigation = () => {
    const { topNav, navCat, navLeft, navRight } = style;
    

    return (
        <nav id={topNav}>
            <div class={navLeft}>
                <NavLogo/>
                <span className={navCat}>Categories</span>
                <NavLink className={navCat} to="/available">Shop</NavLink>
                <span className={navCat}>Gallery</span>
            </div>
            <div className={navRight}>
                <NavSearch />
                <UserStatus className={navCat} />
            </div>
        </nav>
    )
}



export default TopNavigation;
