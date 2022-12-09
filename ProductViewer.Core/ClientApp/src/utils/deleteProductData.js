import axios from "axios";

/**
 * Deletes a product from the database
 * @param {string} url - The url of the API endpoint
 * @param {string} id - The id of the product to be deleted
 * @returns {Promise} - Resolves to true if the product was deleted successfully, false otherwise
 * @throws {Error} - If the product was not deleted successfully
 * @throws {Error} - If the API endpoint is not found
 */
const deleteProductData = async (url, data) => {
    try {
        const res = await axios.delete(url+`/${data.deleteObject.ID}`);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default deleteProductData;
