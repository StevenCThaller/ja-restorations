import config from '../config.json';
import jwt from 'jsonwebtoken';
import { TripleDES, mode, pad, enc, MD5 } from 'crypto-js';

const decrypt = (value, hash) => {
    // const has = MD5(hash)
    return TripleDES.decrypt(value, MD5(hash), { mode: mode.ECB, padding: pad.Pkcs7 }).toString(enc.Utf8);
}

const userReducer = (user = {  email: '', firstName: '', lastName: '' }, action) => {
    switch(action.type) {
        case "user/decryptToken":
            const token = jwt.decode(action.payload);
            console.log(token);
            let email = decrypt(token.sub, config.EMAIL_ENCRYPTION);
            let fName = decrypt(token.sub, config.JwtSecret);
            let lName = decrypt(token.sub, config.JwtSecret);
            user = { ...user, email: email, firstName: fName, lastName: lName };
            console.log({ email, fName, lName });
            break;
        case "user/set":
            // const { email: uEmail, firstName: uFName, lastName: uLName } = action.payload;
            user = { ...user, ...action.payload }
            break;
        default:
            break;
    };
    return user;
}

export default userReducer;