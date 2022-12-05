import React, { Children } from "react";
import ScrollToTop from "./ScrollToTop";

const ScrollToBack = ({children, ...props}) => {
    return (
        <div>
            {children}
            <ScrollToTop></ScrollToTop>
        </div>
    )
}

export default ScrollToBack