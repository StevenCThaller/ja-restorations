import React, { useEffect, useState } from 'react'
import { Menu, MenuItem, SubMenu, MenuDivider, MenuButton } from '@szhsin/react-menu';
import '@szhsin/react-menu/dist/index.css';
import { useHistory, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { Link, Route, Switch, useRouteMatch } from 'react-router-dom';
import { userStatus, profilePicture, loggedIn } from '../UserStatus/UserStatus.module.css';
import profileDefault from '../../assets/images/pfpDefault.png';
import NewFurniture from '../../components/NewFurniture'
import FurnitureForm from '../../containers/FurnitureForm';
import GalleryImageForm from '../../containers/GalleryImageForm';

const UserMenu = props => {
    const history = useHistory();
    const [showCreateFurniture, setShowCreateFurniture] = useState(false);
    const [showCreateGallery, setShowCreateGallery] = useState(false);
    const { user } = props;
    const { path, url } = useRouteMatch();
    // useEffect(() => {

    // }, [showCreate])
    
    return (
        <>
            <Menu menuButton={
                <div className={userStatus}>
                    <img className={profilePicture} src={user.profilePicture ? user.profilePicture : profileDefault} alt="No Profile Picture" />
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
                        <MenuItem onClick={() => setShowCreateFurniture(s => !s)}>Create Furniture</MenuItem>
                        <MenuItem onClick={() => setShowCreateGallery(s => !s)}>Add Gallery Image</MenuItem>
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
            <NewFurniture
                title="Create New Furniture"
                body={<FurnitureForm />}
                show={showCreateFurniture}
                setShow={setShowCreateFurniture}
            />
            <NewFurniture 
                title="Upload Images to Gallery"
                body={<GalleryImageForm />}
                show={showCreateGallery}
                setShow={setShowCreateGallery}
            />
        </>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(UserMenu))
