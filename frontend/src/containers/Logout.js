import { useEffect } from 'react';
import { connect } from 'react-redux';
import { withRouter, Redirect } from 'react-router-dom';
import { logout } from '../actions/authActions';

const Logout = props => {
    const { auth, logout } = props;
    useEffect(() => {
        logout();
        
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
        logout: () => dispatch(logout())
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Logout))
