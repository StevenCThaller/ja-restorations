const initialFurniture = {
    name: '',
    description: '',
    type: '',
    priceFloor: 0,
    priceCeiling: 0,
    width: '',
    length: '', 
    height: '',
    estimatedWeight: '',
    likedByUsers: []
}
const furnitureReducer = (furniture = initialFurniture, action) => {
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
                [payload.target.name]: parseFloat(payload.target.value)
            }
            return furniture;
        case "furniture/reset":
            furniture = initialFurniture;
            return furniture;
        default:
            return furniture;
    }
}

export default furnitureReducer;