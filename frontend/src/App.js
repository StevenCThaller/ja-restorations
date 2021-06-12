import './App.css';
import { Route, Switch, withRouter } from 'react-router-dom';
import Login from './containers/Login';
import Logout from './containers/Logout';
import TopNavigation from './components/TopNavigation/TopNavigation';
import Home from './components/Home';
import { connect } from 'react-redux';
import FurnitureForm from './containers/FurnitureForm';
import AvailableNow from './containers/AvailableNow';
import ProtectedRoute from './containers/ProtectedRoute';
import Gallery from './containers/Gallery/Gallery';
// import 'bootstrap/dist/css/bootstrap.min.css';
import UserProfile from './containers/UserProfile/UserProfile';
import { useEffect, useState } from 'react';
import { CookieIsValid, GetHeaders } from './services/authService';
import { login, logout } from './actions/authActions';
import { clearUser, setUser } from './actions/userActions';
import axios from 'axios';
import jwt from 'jsonwebtoken';


const App = props => {
  const { auth, login, logout, setUser, clearUser } = props;
  const [loaded, setLoaded] = useState(false);
  useEffect(async () => {
    // localStorage.clear();
    if(await CookieIsValid()){
      let cookie = localStorage.getItem('jatoken');
      login(cookie);
      let token = jwt.decode(cookie);
      axios.get(`http://localhost:5000/api/users/${token.UserId}`, { headers: { Authorization : `Bearer ${cookie}`}})
        .then(response => {
          let user = response.data.value.results;
          if(user !== null){
            setUser(user);
          } else {
            logout();
            clearUser();
            return Promise.reject('no');
          }
          setLoaded(true);
        })
        .catch(err => {
          logout();
          clearUser();
          setLoaded(true);
        })
    } else {
      logout();
      clearUser();
      setLoaded(true);
    }
  }, [])

  return (
    <>
    <TopNavigation />
    {
      loaded ?
      <>
        <Switch>
          <Route exact path="/" component={Home}/>
          <Route path="/login" component={Login} />
          <Route path="/logout" component={Logout} />
          <Route path="/available" component={AvailableNow}/>
          <Route path="/gallery" component={Gallery}/>
          <ProtectedRoute level={2} path="/addfurniture">
            <FurnitureForm/>
          </ProtectedRoute>
          <ProtectedRoute level={1} path="/account">
            <UserProfile />
          </ProtectedRoute>
        </Switch>
      </>
      :
      ''
      }
    </>
  );
}

const mapStateToProps = state => {
  return {
    auth: state.auth
  }
}

const mapDispatchToProps = dispatch => {
  return {
    login: token => {
      dispatch(login(token));
    },
    logout: () => {
      dispatch(logout());
    },
    setUser: token => {
      dispatch(setUser(token));
    },
    clearUser: () => {
      dispatch(clearUser());
    }
  }
}


export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));
