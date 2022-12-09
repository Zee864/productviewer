import axios from "axios";

/**
 * Gets the data of a product from the database
 * @param {string} url - The url of the API endpoint
 * @returns {Promise} - Resolves to the data of the product if the product was found, false otherwise
 * @throws {Error} - If the product was not found
 * @throws {Error} - If the API endpoint is not found
 * @throws {Error} - If the data is not valid
 * @throws {Error} - If the data is not in the correct format
 */
const getProductData = async (url) => {
    try {
        const res = await axios.get(url);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default getProductData;