import axios from "axios";

const deleteProductData = async (url, data) => {
    try {
        const res = await axios.delete(url+`/${data.deleteObject.ID}`);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default deleteProductData;
