import React from 'react';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import Product from "./components/Product";
import "./styles/global.css";

const App =() => {
    return (
        <Router>
            <Switch>
                <>
                    <Sidebar />
                    <Route exact path="/" component={Product}></Route>
                </>
            </Switch>
        </Router>
    );
}

export default App;
