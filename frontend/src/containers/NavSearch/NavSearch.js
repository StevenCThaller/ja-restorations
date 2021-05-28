import React from 'react'
import style from './NavSearch.module.css';
import search from '../../assets/images/search.png';

const NavSearch = () => {
    const { navSearch, searchIcon } = style;
    const searchHandler = e => {
        e.preventDefault();
        console.log("hello");
    }
    return (
        <form onSubmit={searchHandler} className={navSearch}>
            <input type="search" placeholder="Search"/>
            <button type="submit">
                <img className={searchIcon} src={search} alt="Search button" />
            </button>
        </form>
    )
}

export default NavSearch
