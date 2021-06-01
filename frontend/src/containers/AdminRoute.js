import { connect } from 'react-redux';
import { Redirect, Route, withRouter } from 'react-router-dom';

const AdminRoute = props => {
    const { user, children, ...rest } = props;
    console.log(user.role);
    return (
        <Route 
            { ...rest }
            render={({ location }) =>
                user.role >= 3 ? (
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

export default withRouter(connect(mapStateToProps)(AdminRoute));
