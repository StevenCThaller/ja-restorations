const modalReducer = (modal = { show: false, modal: '' }, action) => {
    switch(action.type) {
        case "modal/show":
            modal = { ...modal, show: true, modal: action.payload };
            break;
        case "modal/hide":
            modal = { ...modal, show: false, modal: action.payload };
            break;
        default: 
            break;
    };
    return modal;
}

export default modalReducer;