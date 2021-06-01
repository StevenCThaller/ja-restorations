import config from '../config.json';
import jwt from 'jsonwebtoken';
import { TripleDES, mode, pad, enc, MD5 } from 'crypto-js';

const decrypt = (value, hash) => {
    // const has = MD5(hash)
    return TripleDES.decrypt(value, MD5(hash), { mode: mode.ECB, padding: pad.Pkcs7 }).toString(enc.Utf8);
}

const initialUser = {
    email: '',
    firstName: '', 
    lastName: '',
    role: '',
    userId: ''
}

const userReducer = (user = initialUser, action) => {
    switch(action.type) {
        case "user/decryptToken":
            const token = jwt.decode(action.payload);
            console.log(token);
            let tEmail = decrypt(token.sub, config.EMAIL_ENCRYPTION);
            // let tEmail = decrypt(token.sub, config.EMAIL_ENCRYPTION);
            let tFName = decrypt(token.sub, config.JwtSecret);
            let tLName = decrypt(token.sub, config.JwtSecret);

            user = { ...user, email: tEmail, firstName: tFName, lastName: tLName };
            break;
        case "user/set":
            console.log(action.payload);
            const { email, firstName, lastName, roleId, userId } = action.payload;
            user = { ...user, email, firstName, lastName, role: roleId, userId }
            break;
        case "user/clear":
            user = initialUser;
            break;
        default:
            break;
    };
    return user;
}

export default userReducer;