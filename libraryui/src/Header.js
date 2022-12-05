import React, { useState } from "react";
import PostFormBook from "./components/PostCreate/PostFormBook";
import PostFormNewsPaper from "./components/PostCreate/PostFormNewsPaper";
import PostFormPatent from "./components/PostCreate/PostFormPatent";
import SignIn from "./components/Sign/SignIn";
import SignUp from "./components/Sign/SignUp";
import MyButton from "./components/UI/Buttons/MyButton";
import Modal from "./components/UI/Modal/Modal";
import './styles/Header.css';

function Header(){

    const [modal, setModal] = useState({
        modelBook:false,
        modelNewsPaper:false,
        modelPatent:false,
        modelUsers:false,
        modelSignUp:false,
        modelSignIn:false
    });

    function closeForm(){
        window.location.reload();
    } 
     
    return (
        <div>
            <nav>
                <ul className="left-nav-bar">
                    <li><a href="/#">Catalog</a></li>
                    <li><a href="/#" onClick={() => setModal({modelBook:true})}>Book</a></li>
                    <li><a href="/#" onClick={() => setModal({modelNewsPaper:true})}>NewsPaper</a></li>
                    <li><a href="/#" onClick={() => setModal({modelPatent:true})}>Patent</a></li>
                    <li><a href="/#" onClick={() => setModal({modelUsers:true})}>Users</a></li>
                </ul>
                <ul className="right-nav-bar">
                    <li><a href="/#" onClick={() => setModal({modelSignIn:true})}>SignIn</a></li>
                    <li><a href="/#" onClick={() => setModal({modelSignUp:true})}>SignUp</a></li>
                </ul>
            </nav>
            <Modal visible = {modal.modelBook} setVisible = {setModal}>
                <div onClick={() => setModal({modelBook:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Create Book</h2>
                <PostFormBook></PostFormBook>
            </Modal>

            <Modal visible = {modal.modelNewsPaper} setVisible = {setModal}>
                <div onClick={() => setModal({modelNewsPaper:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Create NewsPaper</h2>
                <PostFormNewsPaper></PostFormNewsPaper>
            </Modal>

            <Modal visible = {modal.modelPatent} setVisible = {setModal}>
                <div onClick={() => setModal({modelPatent:false})}>
                    <MyButton onClick={closeForm}>Close</MyButton>
                </div>
                <h2>Create Patent</h2>
                <PostFormPatent></PostFormPatent>
            </Modal>

            <Modal visible = {modal.modelUsers} setVisible = {setModal}>
                <div onClick={() => setModal({modelPatent:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>Users</h2>
            </Modal>

            <Modal visible = {modal.modelSignUp} setVisible = {setModal}>
                <div onClick={() => setModal({modelSignUp:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>Sign Up</h2>
                <SignUp></SignUp>
            </Modal>

            <Modal visible = {modal.modelSignIn} setVisible = {setModal}>
                <div onClick={() => setModal({modelSignIn:false})}>
                    <MyButton>Close</MyButton>
                </div>
                <h2>Sign In</h2>
                <SignIn></SignIn>
            </Modal>
        </div>
    );
}

export default Header;