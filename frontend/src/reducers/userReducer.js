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
    role: '',
    userId: '',
    profilePicture: ''
}

const userReducer = (user = initialUser, action) => {
    switch(action.type) {
        case "user/decryptToken":
            const token = jwt.decode(action.payload);
            // console.log(token);
            let tEmail = decrypt(token.sub, config.EMAIL_ENCRYPTION);
            // let tEmail = decrypt(token.sub, config.EMAIL_ENCRYPTION);
            // let tFName = decrypt(token.sub, config.JwtSecret);
            // let tLName = decrypt(token.sub, config.JwtSecret);

            user = { ...user, email: tEmail };
            break;
        case "user/set":
            // console.log(action.payload);
            const { email, firstName, roleId, userId, profilePicture } = action.payload;
            user = { ...user, firstName, email, role: roleId, userId, profilePicture }
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