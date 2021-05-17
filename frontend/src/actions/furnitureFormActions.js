export const textHandler = target => {
    return dispatch => {
        dispatch({
            type: 'furniture/text',
            payload: target
        })
    }
}
export const numberHandler = target => {
    return dispatch => {
        dispatch({
            type: 'furniture/number',
            payload: target
        })
    }
}