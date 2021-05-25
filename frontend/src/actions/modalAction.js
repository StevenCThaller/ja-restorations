export const show = modal => {
    return dispatch => {
        dispatch({
            type: "modal/show",
            payload: modal
        })
    }
}

export const hide = () => {
    return dispatch => {
        dispatch({
            type: "modal/hide",
            payload: ""
        })
    }
}