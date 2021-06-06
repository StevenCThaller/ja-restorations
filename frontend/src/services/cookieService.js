export const setCookie = payload => localStorage.setItem('jatoken', payload);

export const deleteCookie = () => localStorage.removeItem('jatoken');

export const getCookie = () => localStorage.getItem('jatoken');