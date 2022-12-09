import axios from "axios";

const getProductData = async (url) => {
    try {
        const res = await axios.get(url);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default getProductData;