import React from 'react'
import { NavLink } from 'react-router-dom';
import { navCat, loggedIn } from './TopNavigation/TopNavigation.module.css';
import UserMenu from '../containers/UserMenu/UserMenu';

const NavUser = props => {
    const { email, role } = props;
    return (
        <>
            {/* <span id={loggedIn} className={ navCat }>
                <NavLink to='/placeholder'>{email.slice(0, email.indexOf('@'))}</NavLink>
            </span> */}
            <UserMenu/>
        </>
    )
}

export default NavUser
