export const resetImages = target => {
    return dispatch => {
        dispatch({
            type: 'image/reset',
            payload: {}
        })
    }
}

export const resetDisplayImages = () => {
    return dispatch => {
        dispatch({
            type: 'image/display/reset',
            payload: []
        })
    }
}