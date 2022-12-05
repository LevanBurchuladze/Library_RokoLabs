import React from 'react';
import cl from './Modal.module.css';


const Modal = ({children,visible, setVisible}) => {

    const rootClasses = [cl.myModal];

    if(visible){
        rootClasses.push(cl.active);
    }

    return(
        <div className = {rootClasses.join(' ')}>
            <div className = {cl.myModalContent}>
                {children}
            </div>
        </div>
    )
}

export default Modal