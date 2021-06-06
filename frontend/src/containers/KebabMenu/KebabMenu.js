import React from 'react'
import { Menu, MenuItem, SubMenu, MenuDivider, MenuButton } from '@szhsin/react-menu';
import '@szhsin/react-menu/dist/index.css';
import { useHistory, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { NavLink } from 'react-router-dom';
import { userStatus, profilePicture, loggedIn } from '../UserStatus/UserStatus.module.css';
import profileDefault from '../../assets/images/pfpDefault.png'

const KebabMenu = props => {
    const history = useHistory();
    const { user } = props;
    
    return (
        <Menu menuButton={
            <div className={userStatus}>
                <img className={profilePicture} src={profileDefault} alt="No Profile Picture" />
                <span>{
                    user.firstName ?
                    user.firstName
                    :
                    user.email.slice(0, user.email.indexOf('@'))
                }</span>
            </div>
        }>
            <MenuItem onClick={() => history.push('/account')}>Account</MenuItem>
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
