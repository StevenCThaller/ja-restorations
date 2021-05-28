/*
    I was following a guide for most of this authentication stuff. Then at one point
    I looked at a second one, then once it was all working I realized I had this code 
    in here that isn't used. But I could definitely see it coming in handy, so I'll 
    leave it in for now.
*/

import jwt from 'jsonwebtoken';

export const UserIsValid = token => {
    if (token.isAuthenticated) {
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true: false;
    } 
    return false;
}

export const UserIsEmployee = token => {
    if (token.roleId > 1){
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true : false;
    }
    return false;
}

export const CookieIsValid = () => {
    const cookie = jwt.decode(localStorage.getItem('jatoken'));
    // if()
    
}