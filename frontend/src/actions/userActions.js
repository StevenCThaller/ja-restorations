export const decryptToken = token => {
    return dispatch => {
        dispatch({
            type: "user/decryptToken",
            payload: token
        })
    }
}

export const setUser = user => {
    return dispatch => {
        dispatch({
            type: "user/set",
            payload: user
        })
    }
}

export const clearUser = () => {
    return dispatch => {
        dispatch({
            type: "user/clear",
            payload: ""
        })
    }
}