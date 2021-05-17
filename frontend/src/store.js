import { createStore, combineReducers, applyMiddleware } from 'redux';
import { createLogger } from 'redux-logger';
import thunk from 'redux-thunk';
import auth from './reducers/authReducer';
import user from './reducers/userReducer';
import furnForm from './reducers/furnitureFormReducer';

export default createStore(combineReducers({
    auth, user, furnForm
    }),
    {},
    applyMiddleware(createLogger(), thunk)
);
