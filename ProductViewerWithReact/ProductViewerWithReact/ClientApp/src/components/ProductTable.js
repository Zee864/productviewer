import { useState, useEffect } from "react";
import MaterialTable from "material-table";
import CircularProgress from '@material-ui/core/CircularProgress';
import { makeStyles } from "@material-ui/core/styles";
import Alert from "@mui/material/Alert";
import Stack from "@mui/material/Stack";
import Snackbar from "@mui/material/Snackbar";
import CategoryFilter from "./CategoryFilter";
import FilterByCategoryToggle from "./FilterByCategoryToggle";
import ResetToggle from "./ResetToggle";
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
    // Define the columns used in the table
    // options for this can be found on https://material-table.com/#/docs/all-props under columns section
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
            type: "numeric",
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
    const [productTableData, setProductTableData] = useState({});
    const [error, setError] = useState(false);
    const [open, setOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [severity, setSeverity] = useState("error");
    const [isLoading, setIsLoading] = useState(true);
    const classes = useStyles();

    useEffect(() => {
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
        if (reason === "clickaway") {
            return;
        }

        setError(false);
    };
    
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
            {!isLoading ?
                <>
                    {productTableData &&
                    Object.keys(productTableData).length !== 0 &&
                    Object.getPrototypeOf(productTableData) !== Object.prototype ? (
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
                            <ResetToggle setProductTableData={setProductTableData}/>
                            <FilterByCategoryToggle open={open} setOpen={setOpen}/>
                            {open && (
                                <CategoryFilter
                                    setProductTableData={setProductTableData}
                                    setError={setError}
                                    setAlertMessage={setAlertMessage}
                                    setSeverity={setSeverity}
                                />
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
                                    headerStyle: {
                                        backgroundColor: "rgba(232, 232, 232, 1)",
                                        color: "black",
                                    },
                                }}
                            />
                        </>
                    ) : (
                        <></>
                    )} 
                </>
                :(
                    <CircularProgress className={"spinner"} size={60} thickness={4}/>
                )}
            </div>
    );
};

export default ProductTable;
