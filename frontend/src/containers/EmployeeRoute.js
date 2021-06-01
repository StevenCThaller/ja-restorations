import { connect } from 'react-redux';
import { Redirect, Route, withRouter } from 'react-router-dom';

const EmployeeRoute = props => {
    const { user, children, component, ...rest } = props;
    console.log(user.role);

    return (
        <Route 
            { ...rest }
            render={({ location }) =>
                user.role >= 2 ? (
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

export default withRouter(connect(mapStateToProps)(EmployeeRoute));
