import { connect } from 'react-redux';
import { Redirect, Route, withRouter } from 'react-router-dom';

const ProtectedRoute = props => {
    const { user, children, component, level, ...rest } = props;
    return (
        <Route 
            { ...rest }
            render={({ location }) =>
                user.role >= level ? (
                    children
                ) : (
                    <Redirect to={{ pathname: "/", state: { from: location }}}/>
                )
            }
        >
        </Route>
    )
}

const mapStateToProps = state => {
    return {
        user: state.user
    }
}

export default withRouter(connect(mapStateToProps)(ProtectedRoute));
