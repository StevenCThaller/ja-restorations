import { GetHeaders } from './authService';
import axios from 'axios';

export const GetAccount = ({id, auth}) => axios.get(`http://localhost:5000/api/users/${id}/account`, GetHeaders(auth));