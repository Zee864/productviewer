import axios from "axios";

/**
 * Creates a new product in the database
 * @param {string} url - The url of the API endpoint
 * @param {object} data - The data of the product to be created
 * @returns {Promise} - Resolves to true if the product was created successfully, false otherwise
 * @throws {Error} - If the product was not created successfully
 * @throws {Error} - If the API endpoint is not found
 * @throws {Error} - If the data is not valid
 * @throws {Error} - If the data is not in the correct format
 * @throws {Error} - If the data is not in the correct type
 */
const createNewProduct = async (url, data) => {
    try {
        const res = await axios.post(url, data.newObject);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default createNewProduct;