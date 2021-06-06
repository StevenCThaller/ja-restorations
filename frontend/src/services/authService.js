/*
    I was following a guide for most of this authentication stuff. Then at one point
    I looked at a second one, then once it was all working I realized I had this code 
    in here that isn't used. But I could definitely see it coming in handy, so I'll 
    leave it in for now.
*/

import jwt from 'jsonwebtoken';
import axios from 'axios';

export const UserIsValid = token => {
    if (token.isAuthenticated) {
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true: false;
    } 
    return false;
}

export const UserIsAdmin = token => {
    if (token.isAdmin) {
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true : false;
    }
    return false;
}

export const UserIsEmployee = token => {
    if(token.isEmployee) {
        let decodedToken = jwt.decode(token.user);
        let dateNow = new Date();
        return decodedToken.exp > dateNow.getTime() / 1000 ? true : false;
    }
    return false;
}

export const CookieIsValid = async () => {
    try
    {

        // console.log(localStorage);
        if('jatoken' in localStorage) {
            const token = localStorage.getItem('jatoken');
            const cookie = jwt.decode(token);
            
            let result = await axios.get(`http://localhost:5000/api/users/${cookie.UserId}/verify`, { headers: { Authorization: `Bearer ${token}`}})
            console.log(result);
            if(result.data.value.results){
                return true;
            } else {
                return false;
            }
        } else{
            return false;
        }
    }
    catch(err){
        localStorage.clear();
        return false;
    }
}

export const GetHeaders = auth => ({ headers: { Authorization: `Bearer ${auth.user}`}})