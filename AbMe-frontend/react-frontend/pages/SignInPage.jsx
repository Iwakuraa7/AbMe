import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function SingInPage() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [apiResponse, setApiResponse] = useState({});
    const navigate = useNavigate();

    async function signInUser(e) {
        e.preventDefault();
        
        const response = await fetch("http://localhost:5078/api/account/login", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}`,
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
            localStorage.setItem('token', data.userInfo.token);
            console.log(data.message);
            console.log(data);
        }
        else {
            console.log(data.message);
        }
    }

    useEffect(() => {
        if(localStorage.getItem('token') !== null) {
            navigate('/logout');
        }
    })

    useEffect(() => {
        if(apiResponse.succeeded) {
            navigate('/home');
        }
    }, [apiResponse])

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