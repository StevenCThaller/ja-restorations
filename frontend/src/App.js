import './App.css';
import { Route, Switch, withRouter } from 'react-router-dom';
import Login from './containers/Login';
import Logout from './containers/Logout';
import TopNavigation from './components/TopNavigation/TopNavigation';
import Home from './components/Home';
import { connect } from 'react-redux';
import FurnitureForm from './containers/FurnitureForm';
// import 'bootstrap/dist/css/bootstrap.min.css';


const App = props => {
  const { auth } = props;
  

  return (
    <div className="App">
      <TopNavigation />
      <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/login" component={Login} />
        <Route path="/logout" component={Logout} />
        <Route path="/addfurniture" component={FurnitureForm}/>
      </Switch>
    </div>
  );
}

const mapStateToProps = state => {
  return {
    auth: state.auth
  }
}


export default withRouter(connect(mapStateToProps)(App));
