import Button from "react-bootstrap/Button";
import "../styles/FilterByCategoryToggle.css";

const FilterByCategoryToggle = ({ open, setOpen }) => {
    const showForm = () => {
        setOpen(!open);
    };
    let message = "";
    if (open) message = "Close";
    else message = "Filter By Category";

    return (
        <Button variant="dark" id="date-form-toggle" onClick={showForm}>
            {message}
        </Button>
    );
};

export default FilterByCategoryToggle;
