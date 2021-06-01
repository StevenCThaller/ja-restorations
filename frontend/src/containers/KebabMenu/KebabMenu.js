import React from 'react'
import { Menu, MenuItem, SubMenu, MenuDivider, MenuButton } from '@szhsin/react-menu';
import '@szhsin/react-menu/dist/index.css';
import { useHistory, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { NavLink } from 'react-router-dom';
import { navCat } from '../../components/TopNavigation/TopNavigation.module.css';

const KebabMenu = props => {
    const history = useHistory();
    const { user } = props;
    
    return (
        <Menu menuButton={
            // <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-three-dots-vertical" viewBox="0 0 16 16">
            //     <path d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
            // </svg>
            <span className={navCat}>{user.email.slice(0, user.email.indexOf('@'))}</span>
        }>
            <MenuItem>Thing One</MenuItem>
            <MenuItem>Thing Two</MenuItem>
            <MenuItem>Thing Three</MenuItem>
            <MenuDivider />
            {
                user.role > 1 ?
                <SubMenu className="right-side-submenu" label={ user.role == 2 ? "Employee" : "Admin" } direction="left">
                    <MenuItem onClick={() => history.push('/addfurniture')}>Create Furniture</MenuItem>
                </SubMenu>
                :
                <SubMenu className="right-side-submenu" label="User">
                    <MenuItem onClick={() => history.push('/placeholder')}>Go To thing</MenuItem>
                    <MenuItem onClick={() => history.push('/placeholder')}>Go To thing</MenuItem>
                    <MenuItem onClick={() => history.push('/placeholder')}>Go To thing</MenuItem>
                </SubMenu>
            }
            <MenuDivider />
            <MenuItem onClick={() => history.push('/logout')}>Log Out</MenuItem>
        </Menu>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(KebabMenu))
