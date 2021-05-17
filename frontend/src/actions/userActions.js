export const decryptToken = token => {
    return dispatch => {
        dispatch({
            type: "user/decryptToken",
            payload: token
        })
    }
}