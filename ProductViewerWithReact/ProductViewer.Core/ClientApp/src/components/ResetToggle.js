import Button from "react-bootstrap/Button";
import getProductData from "../utils/getProductData";
import "../styles/ResetToggle.css";

const ResetToggle = ({ setEmployeeTableData }) => {
    const resetTable = async () => {
        const defaultEmployeeData = await getProductData(
            "product"
        );
        setEmployeeTableData(defaultEmployeeData);
    };
    return (
        <Button variant="dark" onClick={resetTable} id="reset-toggle">
            Reset
        </Button>
    );
};

export default ResetToggle;
