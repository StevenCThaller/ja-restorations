import axios from 'axios';

const hashUrlCode = string => {
    var hash = 0, i, chr;
    let split = string.split('.');
    if (split[0].length === 0) return hash;
    for (i = 0; i < split[0].length; i++) {
        chr   = split[0].charCodeAt(i);
        hash  = ((hash << 10) - hash) + chr;
        hash |= 0; // Convert to 32bit integer
    }
    return hash+'.'+split[1];
};

export const submitGalleryImage = (body, headers) => axios.post('http://localhost:5000/api/images/gallery', body, headers)

export const convertFilesToFormData = images => {
    let formData = new FormData();
    for(let i = 0; i < images.length; i++) {
        formData.append('fileNames', hashUrlCode(images[i].name));
        formData.append('formFiles', images[i]);
    }
    return formData;
}