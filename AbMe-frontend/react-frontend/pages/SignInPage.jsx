import { useState } from "react";

export default function SingInPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    function signInUser(e) {
        e.preventDefault();
        console.log(username, password);
    }

    return(
        <>
        <form onSubmit={signInUser}>
        <div class='authCard'>
            <div class='authMessageBox'>
                <h2>Welcome back</h2>
            </div>
            <div class='inputFieldsBox'>
                <div>
                    <label for='usernameField'>Username</label><br/>
                    <input onChange={(e) => {setUsername(e.target.value)}} id='usernameField' type='text'/>
                </div>

                <div>
                    <label for='passwordField'>Password</label><br/>
                    <input onChange={(e) => {setPassword(e.target.value)}} id='passwordField' type='password'/>
                </div>
            </div>
            <button type='submit'>Sign in</button>
        </div>
        </form>
        </>
    )
}