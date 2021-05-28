import { setCookie, deleteCookie } from '../services/cookieService';

const authReducer = (auth = { user: '', isAuthenticated: false }, action) => {
    switch (action.type) {
        case "auth/login":
            auth = { ...auth, user: action.payload, isAuthenticated: true, roleLevel: 1 };
            // setCookie(action.payload);
            break;
        case "auth/logout": 
            auth = { ...auth, user: '', isAuthenticated: false };
            deleteCookie();
            break;
        default:
            break;
    };
    return auth;
}

export default authReducer;