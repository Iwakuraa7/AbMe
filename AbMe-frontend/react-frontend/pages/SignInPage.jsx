import { useState } from "react";

export default function SingInPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [apiResponse, setApiResponse] = useState({});

    async function signInUser(e) {
        e.preventDefault();
        
        const response = await fetch("http://localhost:5078/api/account/login", {
            method: "POST",
            headers: {
                "Accept": "*/*",
                "Content-Type": "application/json"
            },
            body : JSON.stringify({
                Username: username,
                password: password
            })
        })

        const data = await response.json();

        if(data.succeeded) {
            setApiResponse(data);
            console.log(data.message);
        }
        else {
            console.log(data.message);
        }
    }

    return(
        <>
        <form onSubmit={signInUser}>
        <div className='authCard'>
            <div className='authMessageBox'>
                <h2>Welcome back</h2>
            </div>
            <div className='inputFieldsBox'>
                <div>
                    <label htmlFor='usernameField'>Username</label><br/>
                    <input onChange={(e) => {setUsername(e.target.value)}} id='usernameField' type='text'/>
                </div>

                <div>
                    <label htmlFor='passwordField'>Password</label><br/>
                    <input onChange={(e) => {setPassword(e.target.value)}} id='passwordField' type='password'/>
                </div>
            </div>
            <button type='submit'>Sign in</button>
        </div>
        </form>
        </>
    )
}