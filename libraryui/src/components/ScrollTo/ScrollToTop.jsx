import React, { useEffect, useState } from "react";
import cl from './ScrollButton.module.css';

const ScrollToTop = () => {
    const [backToTop, setbackToTop] = useState(false);

    useEffect(() => {
        window.addEventListener("scroll", () =>{
            if(window.scrollY > 50){
                setbackToTop(true);
            }
            else{
                setbackToTop(false);
            }
        })
    },[]);

    const scrollUp = () => {
        window.scrollTo({
            top:0,
            behavior: "auto"
        })
    }

    return (
        <div>
            {
                backToTop &&( <button className={cl.btn} onClick={scrollUp}>^</button>)
            }
        </div>
    )
}

export default ScrollToTop