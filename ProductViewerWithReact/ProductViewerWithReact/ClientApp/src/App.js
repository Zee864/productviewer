import React from 'react';
import { BrowserRouter as Router, Route, Routes, Switch } from "react-router-dom";
import AppRoutes from './AppRoutes';
import "./styles/global.css";
import Sidebar from "./components/Sidebar";
import Product from "./components/Product";

const App =() => {
    return (
        <Router>
            <Switch>
                <>
                    <Sidebar />
                    <Route exact path="/" component={Product}></Route>
                    {/*<Route>*/}
                    {/*    {AppRoutes.map((route, index) => {*/}
                    {/*        const { element, ...rest } = route;*/}
                    {/*        return <Route key={index} {...rest} element={element} />;*/}
                    {/*    })}*/}
                    {/*</Route>*/}
                </>
            </Switch>
        </Router>
    );
}

export default App;
