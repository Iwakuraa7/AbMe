import { useState } from "react"

export default function SignUpPage() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    function registerNewUser(e) {
        e.preventDefault();
        console.log(username, email, password);
    }

    return(
        <>
        <form onSubmit={registerNewUser}>
        <div class='authCard'>
            <div class='authMessageBox'>
                <h2>Welcome to Abme</h2>
            </div>
            <div class='inputFieldsBox'>
                <div>
                    <label for='usernameField'>Username</label><br/>
                    <input onChange={(e) => {setUsername(e.target.value)}} id='usernameField' type='text'/>
                </div>

                <div>
                    <label for='emailField'>Email</label><br/>
                    <input onChange={(e) => {setEmail(e.target.value)}} id='emailField' type='email'/>
                </div>

                <div>
                    <label for='passwordField'>Password</label><br/>
                    <input onChange={(e) => {setPassword(e.target.value)}} id='passwordField' type='password'/>
                </div>
            </div>
            <button type='submit'>Create new account</button>
        </div>
        </form>
        </>
    )
}