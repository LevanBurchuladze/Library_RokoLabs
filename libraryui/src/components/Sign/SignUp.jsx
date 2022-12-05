import React from "react";
import MyButton from "../UI/Buttons/MyButton";
import MyInput from "../UI/Inputs/MyInput";

const SignUp = () => {
    return (
        <form>
            <p><label>Login</label></p>
            <MyInput type="text"></MyInput>
            
            <p><label>Password</label></p>
            <MyInput type="password" name="password" autoComplete="on"></MyInput>

            <p><label>Confirm Password</label></p>
            <MyInput type="password" name="password" autoComplete="on"></MyInput>

            <MyButton>SingIn</MyButton>
        </form>
    );
};

export default SignUp;