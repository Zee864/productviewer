import { useState, useEffect } from "react";
import MaterialTable from "material-table";
import CircularProgress from '@material-ui/core/CircularProgress';
import { makeStyles } from "@material-ui/core/styles";
import Alert from "@mui/material/Alert";
import Stack from "@mui/material/Stack";
import Snackbar from "@mui/material/Snackbar";
import getProductData from "../utils/getProductData";
import createNewProduct from "../utils/createNewProduct";
import deleteProductData from "../utils/deleteProductData";
import updateProductData from "../utils/updateProductData";
import "../styles/ProductTable.css";

const useStyles = makeStyles({
    errorAlert: {
        backgroundColor: "#212529",
        color: "white",
    },
});

const ProductTable = () => {
    // Define the columns used in the table along with their options
    const columns = [
        {
            title: "ID",
            field: "ID",
            editable: "never",
            validate: (rowData) =>
                !rowData.ID
                    ? { isValid: false, helperText: "ID cannot be empty" }
                    : true,
        },
        {
            title: "Name",
            field: "Name",
            validate: (rowData) =>
                !rowData.Name
                    ? { isValid: false, helperText: "Name cannot be empty" }
                    : true,
        },
        {
            title: "Price",
            field: "Price",
            type: "currency",
            currencySetting: { currencyCode: 'ZAR', minimumFractionDigits: 2, maximumFractionDigits: 2 },
            validate: (rowData) =>
                !rowData.Price
                    ? { isValid: false, helperText: "Price cannot be empty" }
                    : true,
        },
        {
            title: "Category",
            field: "Category",
            type: "numeric",
            validate: (rowData) =>
                !rowData.Category
                    ? { isValid: false, helperText: "Category cannot be empty" }
                    : true,
        }
    ];
    // Contains the data to be displayed in the table
    const [productTableData, setProductTableData] = useState({});
    // Indicates whether there is an error in the data
    const [error, setError] = useState(false);
    // Contains the error message to be displayed in the alert
    const [alertMessage, setAlertMessage] = useState("");
    // Indicates the severity of the error
    const [severity, setSeverity] = useState("error");
    // Used to display the progress indicator
    const [isLoading, setIsLoading] = useState(true);
    const classes = useStyles();

    useEffect(() => {
        // Fetch the data from the API
        getProductData("product")
            .then((data) => {
                setProductTableData(data);
                setIsLoading(false);
            })
            .catch((error) => {
                console.log(`An error occured while getting the data: ${error}`);
            });
    }, []);

    
    const handleClose = (event, reason) => {
        if (reason !== "clickaway") 
            setError(false);
    }
    
    const handleRowDelete = (oldData, resolve) => {
        setTimeout(async () => {
            await deleteProductData(
                "product",
                {deleteObject: oldData}
            ).then((res) => {
                if (res === true) {
                    const deleteIndex = productTableData.findIndex((object) => object.ID === oldData.ID);
                    if (deleteIndex > -1) {
                        productTableData.splice(deleteIndex, 1);
                        setProductTableData([...productTableData]);
                        setError(true);
                        setAlertMessage("Product deleted successfully");
                        setSeverity("success");
                    }

                }
                else {
                    setError(true);
                    setAlertMessage("Error deleting product");
                    setSeverity("error");
                }
            });
            resolve();
        }, 1000);
    }
    
    const handleRowAdd = (newData, resolve) => {
        setTimeout(async () => {
            const res = await createNewProduct(
                "product",
                { newObject: newData }
            );
            
            if(res === true) {
                setProductTableData([...productTableData, newData]);
                setError(true);
                setAlertMessage("Product added successfully");
                setSeverity("success");
            }
            else {
                setError(true);
                setAlertMessage("Product could not be added");
                setSeverity("error");
            }
            
            resolve();
        }, 1000);
    }
    
    const handleRowUpdate = (newData, oldData, resolve) => {
        setTimeout(async () => {
            const res = await updateProductData(
                "product",
                { oldID: oldData.ID, updateObject: newData }
            );
            
            if (res === true) {
                const dataUpdate = [...productTableData];
                dataUpdate.forEach((object, index) => {
                    if (object.ID === oldData.ID) {
                        dataUpdate[index] = newData;
                        return true;
                    }
                });
                
                setProductTableData([...dataUpdate]);
                setError(true);
                setAlertMessage("Product updated successfully");
                setSeverity("success");
            }
            else {
                setError(true);
                setAlertMessage("Product could not be updated");
                setSeverity("error");
            }
            
            resolve();
        }, 1000);
    }

    return (
        <div className="table-container">
            {!isLoading ? (
                <>
                    {error && (
                        <Stack sx={{width: "100%"}} spacing={2}>
                            <Snackbar
                                open={error}
                                autoHideDuration={2500}
                                onClose={handleClose}
                                anchorOrigin={{vertical: "top", horizontal: "center"}}
                            >
                                <Alert
                                    onClose={handleClose}
                                    severity={severity}
                                    sx={{width: "100%"}}
                                    className={classes.errorAlert}
                                >
                                    {alertMessage}
                                </Alert>
                            </Snackbar>
                        </Stack>
                    )}
                    <MaterialTable
                        style={{
                            backgroundColor: "rgba(232, 232, 232, 1)",
                            color: "black",
                        }}
                        title="Products"
                        columns={columns}
                        data={productTableData ? productTableData : []}
                        editable={{
                            onRowUpdate: (newData, oldData) =>
                                new Promise((resolve) => {
                                    handleRowUpdate(newData, oldData, resolve);
                                }),
                            onRowDelete: (oldData) =>
                                new Promise((resolve) => {
                                    handleRowDelete(oldData, resolve)
                                }),
                            onRowAdd: (newData) =>
                                new Promise((resolve) => {
                                    handleRowAdd(newData, resolve)
                                })
                        }}
                        options={{
                            exportButton: true,
                            filtering: true,
                            sorting: true,
                            columnsButton: true,
                            addRowPosition: "first",
                            showEmptyDataSourceMessage: true,
                            showTextRowsSelected: true,
                            tableLayout: "auto",
                            searchAutoFocus: true,
                            headerStyle: {
                                backgroundColor: "rgba(232, 232, 232, 1)",
                                color: "black",
                            },
                        }}
                    />
                </> ) :(
                    <CircularProgress className={"spinner"} size={60} thickness={4}/>
                )}
            </div>
    );
};

export default ProductTable;
