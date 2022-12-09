import axios from "axios";

const createNewProduct = async (url, data) => {
    try {
        const res = await axios.post(url, data.newObject);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default createNewProduct;