import React from "react";
import { Link } from "react-router-dom";
import "../styles/Sidebar.css";

export default function Sidebar() {
    return (
        <>
            <nav className="md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-nowrap md:overflow-hidden shadow-xl bg-white flex flex-wrap justify-between relative md:w-64 z-10 py-4 px-4">
                {/* Brand */}
                <Link to="/">
                    <h4 id="org-name" className="text-blueGray-700">
                        ProductsViewer
                    </h4>
                </Link>
                {/* Divider */}
                <hr className="my-4 md:min-w-full" />
                {/* Heading */}
                <h6 className="md:min-w-full text-blueGray-500 text-xs uppercase font-bold block pt-1 pb-4 no-underline">
                    User Layout Pages
                </h6>
                {/* Handle the navigation
            Each tab is under a list tag 
        */}
                <ul className="md:flex-col md:min-w-full flex flex-col list-none">
                    <li>
                        {/* Links point to the routes set up in App.js */}
                        <Link
                            className={
                                ("text-xs",
                                    "uppercase",
                                    "py-3",
                                    "font-bold",
                                    "block",
                                    window.location.href.indexOf("/") !== -1
                                        ? "text-lightBlue-500 hover:text-lightBlue-600"
                                        : "text-blueGray-700 hover:text-blueGray-500")
                            }
                            to="/"
                        >
                            {/* If tab is selected, highlight it */}
                            <i
                                className={
                                    "fas fa-archive text-lg" +
                                    (window.location.href.indexOf("/") !== -1
                                        ? "opacity-75"
                                        : "text-blueGray-300")
                                }
                            />{" "}
                            Products
                        </Link>
                    </li>
                </ul>
            </nav>
        </>
    );
}
