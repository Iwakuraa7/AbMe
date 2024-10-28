import { useEffect } from "react";
import SignInButton from "../components/SignInButton";
import SignUpButton from "../components/SignUpButton";
import { useNavigate } from "react-router-dom";


export default function HomePage() {
    const navigate = useNavigate();

    useEffect(() => {
        if(localStorage.getItem('token') !== null) {
            navigate('/home');
        }
    });

    return (
        <>
        <h1>AbMe</h1><br/>
        <SignInButton/>
        <SignUpButton/>
        </>
    )
}