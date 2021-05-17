const initialFurniture = {
    name: '',
    description: '',
    type: '',
    priceFloor: '',
    priceCeiling: null,
    width: '',
    length: '', 
    height: '',
    estimatedWeight: ''
}
const furnitureFormReducer = (furniture = initialFurniture, action) => {
    const { type, payload } = action;
    switch(type) {
        case "furniture/text":
            furniture = {
                ...furniture,
                [payload.target.name]: payload.target.value
            }
            return furniture;
        case "furniture/number":
            furniture = {
                ...furniture,
                [payload.target.name]: parseInt(payload.target.value)
            }
            return furniture;
        default:
            return furniture;
    }
}

export default furnitureFormReducer;