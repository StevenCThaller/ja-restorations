import React from 'react'
import { useHistory } from 'react-router-dom';
import logo from '../../assets/images/logo.svg';
import { navLogo } from './NavLogo.module.css';

const NavLogo = () => {
    const history = useHistory();
    const goHome = () =>  history.push('/');
    return <img id={navLogo} src={logo} alt="J&A Restorations Logo" onClick={goHome} />
}

export default NavLogo
