export const login = token => {
    return dispatch => {
        dispatch({
            type: "auth/login",
            payload: token
        })
    }
}

export const logout = () => {
    return dispatch => {
        dispatch({
            type: "auth/logout",
            payload: ""
        })
    }
}