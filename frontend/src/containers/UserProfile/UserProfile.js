import axios from 'axios';
import { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { withRouter } from 'react-router'
import ImagesInput from '../../components/ImagesInput';
import { GetHeaders } from '../../services/authService';
import { GetAccount } from '../../services/userService';
import pfpDefault from '../../assets/images/pfpDefault.png';
import { profilePicture } from './UserProfile.module.css';
import { clearUser, setUser } from '../../actions/userActions';
import { convertFilesToFormData } from '../../services/furnitureService';
import { logout } from '../../actions/authActions';

const UserProfile = props => {
    const { stateUser, setStateUser, auth, logout, clearUser } = props;
    const [user, setUser] = useState({})
    const [loaded, setLoaded] = useState(false);
    const [edit, setEdit] = useState(false);
    const [images, setImages] = useState({});
    const [displayImages, setDisplayImages] = useState([]);


    useEffect(() => {
        GetAccount({id: stateUser.userId, auth})
            .then(response => {
                setUser(response.data.value.results);
                setLoaded(true);
            })
            .catch(err => {
                logout();
                clearUser();
            })
    }, []);


    const changeHandler = e => {
        const { name, value } = e.target;
        setUser({
            ...user,
            [name]: value
        })
    }

    const updateUser = e => {
        e.preventDefault();
        axios.put(`http://localhost:5000/api/users/${stateUser.userId}/edit`, user, GetHeaders(auth))
            .then(response => {
                if(images.hasOwnProperty(0)){
                    return axios.post(`http://localhost:5000/api/images/users/${stateUser.userId}`, convertFilesToFormData(images), GetHeaders(auth))
                }
                else {
                    setUser(response.data.value.results);
                    setStateUser(response.data.value.results);
                    setEdit(false);
                }
            })
            .then(response => {
                if(response !== null) {
                    setUser(response.data.value.results);
                    setStateUser(response.data.value.results);
                    setImages({});
                    setDisplayImages([]);
                    setEdit(false);
                }
            })
            .catch(err => console.log(err));
    }

    const addImages = e => {
        setImages(e.target.files);
        setDisplayImages([...e.target.files]);
    }

    const removeImage = i => {
        setDisplayImages([...displayImages.slice(0, i), ...displayImages.slice(i+1)])
    }


    
    let content = !!loaded ?
    (
        <>
            <h2>Welcome, {stateUser.firstName ? stateUser.firstName : user.email.slice(0, user.email.indexOf('@')) }</h2>
            <h3>Account Info</h3>
            <section>
                {
                    edit ?
                    <form onSubmit={updateUser}>
                        <div>
                            <label htmlFor="firstName">First Name: </label>
                            <input type="text" name="firstName" value={user.firstName} onChange={changeHandler} value={ user.firstName ? user.firstName : '' }/>
                        </div>
                        <div>
                            <label htmlFor="lastName">Last Name: </label>
                            <input type="text" name="lastName" value={user.lastName} onChange={changeHandler} value={ user.lastName ? user.lastName : '' }/>
                        </div>
                        <img className={profilePicture} src={user.profilePicture ? user.profilePicture : pfpDefault} alt={`Profile picture for ${user.firstName ? user.firstName : user.email.slice(0, user.email.indexOf('@'))}`} />
                        <ImagesInput images={displayImages} addImages={addImages} removeImage={removeImage} />
                        <div>
                            <input type="submit" value="Update" />
                            <input type="button" value="Cancel" onClick={ () => setEdit(e => !e) }/>
                        </div>
                    </form>
                    :
                    <>
                    <p>First Name: { user.firstName ? user.firstName : "" }</p>
                    <p>Last Name: { user.lastName ? user.lastName : "" }</p>
                    <img className={profilePicture} src={user.profilePicture ? user.profilePicture : pfpDefault} alt={`Profile picture for ${user.firstName ? user.firstName : user.email.slice(0, user.email.indexOf('@'))}`} />

                    <button onClick={() => setEdit(edit => !edit)}>Edit Info</button>
                    </>
                }
            </section>
        </>
    )
    :
    (
        <p>Shit's not loaded</p>
    )

    return (
        <main>
            {content}
        </main>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth,
        stateUser: state.user
    }
}

const mapDispatchToProps = dispatch => {
    return {
        setStateUser: user => {
            dispatch(setUser(user))
        },
        logout: () => {
            dispatch(logout());
        },
        clearUser: () => {
            dispatch(clearUser());
        }
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(UserProfile));
