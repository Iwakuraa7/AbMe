import { useEffect } from "react";
import LogoutButton from "../components/LogoutButton";
import { useNavigate } from "react-router-dom";

export default function MainPage() {
    const navigate = useNavigate();

    useEffect(() => {
        if(localStorage.getItem('token') === null) {
            return navigate('/');
        }
    })

    return(
        <>
        <h1>Main Page</h1>
        </>
    )
}