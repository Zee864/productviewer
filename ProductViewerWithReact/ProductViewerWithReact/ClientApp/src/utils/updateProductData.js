import axios from "axios";

const updateProductData = async (url, data) => {
    try {
        const res = await axios.put(`${url}/${data.oldID}`, data.updateObject);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default updateProductData;