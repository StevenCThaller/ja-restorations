import jwt from 'jsonwebtoken';

export const UserIsValid = token => {
    console.log("the token is " + token.user);
    if (token.isAuthenticated) {
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true: false;
    } 
    return false;
}

export const CookieIsValid = () => {
    const cookie = jwt.decode(localStorage.getItem('jatoken'));
    // if()
    
}