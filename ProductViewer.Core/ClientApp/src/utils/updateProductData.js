import axios from "axios";

/**
 * Updates a product in the database
 * @param {string} url - The url of the API endpoint
 * @param {object} data - The data of the product to be updated
 * @returns {Pick} - Resolves to true if the product was updated successfully, false otherwise
 * @throws {Error} - If the product was not updated successfully
 * @throws {Error} - If the API endpoint is not found
 * @throws {Error} - If the data is not valid
 * @throws {Error} - If the data is not in the correct format
 */
const updateProductData = async (url, data) => {
    try {
        const res = await axios.put(`${url}/${data.oldID}`, data.updateObject);
        return Promise.resolve(res.data);
    } catch (error) {
        return Promise.reject(error);
    }
};

export default updateProductData;