import React, { useState } from "react";
import PostService from "../../API/PostService";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const SignIn = () => {
    const [log,setLog] = useState('');
    const [pas,setPass] = useState('');

    const signIn = (e) =>{
        e.preventDefault();
        const response = authorize().then((data) => {
            console.log(data);
            console.log(data.data.access_token);
            sessionStorage.setItem("access_token",data.data.access_token);
        });
    }

    async function authorize (){
        return await PostService.authorize(log,pas);
    }

    return (
        <form>
            <p><label>Login</label></p>
            <MyInput onChange={e => setLog(e.target.value)} value={log} type="text" ></MyInput>
            
            <p><label>Password</label></p>
            <MyInput onChange={e => setPass(e.target.value)} value={pas} type="password" name="password" autoComplete="on"></MyInput>

            <MyButton onClick ={signIn}>SingIn</MyButton>
        </form>
    );
};

export default SignIn;