import { useEffect } from 'react';
import { connect } from 'react-redux';
import { withRouter, Redirect } from 'react-router-dom';
import { logout } from '../actions/authActions';
import { clearUser } from '../actions/userActions';

const Logout = props => {
    const { auth, logout, clearUser } = props;
    useEffect(() => {
        logout();
        clearUser();
    }, [auth])
    return (
        <div>
            <Redirect to={{ pathname: "/" }} />
        </div>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth
    };
}

const mapDispatchToProps = dispatch => {
    return {
        logout: () => dispatch(logout()),
        clearUser: () => dispatch(clearUser())
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Logout))
