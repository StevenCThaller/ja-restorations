export const setCookie = payload => localStorage.setItem('jatoken', payload);

export const deleteCookie = () => localStorage.clear();

export const getCookie = () => localStorage.getItem('jatoken');