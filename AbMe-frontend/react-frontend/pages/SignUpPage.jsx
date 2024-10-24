import { useEffect, useState } from "react"

export default function SignUpPage() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    async function registerNewUser(e) {
        e.preventDefault();
        const response = await fetch("http://localhost:5078/api/account/register", {
            method: "POST",
            headers: {
                "Accept": "*/*",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Username: username,
                Email: email,
                Password: password
            })
        });

        const data = await response.json();
        if(data.succeeded) {
            localStorage.setItem('token', data.token);
            console.log(data);
            console.log(data.message);
        }
        else {
            console.log(data.message)
        }
    }

    useEffect(() => {
        if(localStorage.getItem('token') !== null) {
            navigate('/logout');
        }
    })

    return(
        <>
        <form onSubmit={registerNewUser}>
        <div className='authCard'>
            <div className='authMessageBox'>
                <h2>Welcome to Abme</h2>
            </div>
            <div className='inputFieldsBox'>
                <div>
                    <label htmlFor='usernameField'>Username</label><br/>
                    <input onChange={(e) => {setUsername(e.target.value)}} id='usernameField' type='text'/>
                </div>

                <div>
                    <label htmlFor='emailField'>Email</label><br/>
                    <input onChange={(e) => {setEmail(e.target.value)}} id='emailField' type='email'/>
                </div>

                <div>
                    <label htmlFor='passwordField'>Password</label><br/>
                    <input onChange={(e) => {setPassword(e.target.value)}} id='passwordField' type='password'/>
                </div>
            </div>
            <button type='submit'>Create new account</button>
        </div>
        </form>
        </>
    )
}