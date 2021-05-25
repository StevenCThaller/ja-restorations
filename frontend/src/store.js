import { createStore, combineReducers, applyMiddleware } from 'redux';
import { createLogger } from 'redux-logger';
import thunk from 'redux-thunk';
import auth from './reducers/authReducer';
import user from './reducers/userReducer';
import modal from './reducers/modalReducer';
import furn from './reducers/furnitureReducer';

export default createStore(combineReducers({
    auth, user, furn, modal
    }),
    {},
    applyMiddleware(createLogger(), thunk)
);
