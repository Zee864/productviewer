import { useState } from "react";
import PropTypes from "prop-types";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import Box from "@mui/material/Box";
import TableChartIcon from "@mui/icons-material/TableChart";
import ProductTable from "./ProductTable";
import "../styles/Product.css";

const Product = () => {
    //Used for switching between tabs.
    //Tab 1 has a value 1 and so forth...
    //setValue is used to change value variable when tab is clicked on
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <Box id="tab-switcher">
            <Box id="tab-container">
                <Tabs value={value} onChange={handleChange} aria-label="product-data">
                    <Tab
                        icon={<TableChartIcon />}
                        aria-label="table"
                        label="Table View"
                        id="table-tab"
                    />
                </Tabs>
            </Box>
            <TabPanel value={value} index={0}>
                <ProductTable />
            </TabPanel>
        </Box>
    );
};

const TabPanel = (props) => {
    const { children, value, index } = props;

    return <div role="tabpanel">{value === index && children}</div>;
};

// children contains the tab content that is displayed when the tab is clicked on
// index and value are used to identify which tab is clicked
TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.number.isRequired,
};

export default Product;