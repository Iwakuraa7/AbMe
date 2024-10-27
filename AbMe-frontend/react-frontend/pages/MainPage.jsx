import { useEffect, useState } from "react";
import LogoutButton from "../components/LogoutButton";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

export default function MainPage() {
    const [userInfo, setUserInfo] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        if(localStorage.getItem('token') === null) {
            return navigate('/');
        }

        var decodedToken = jwtDecode(localStorage.getItem('token'));
        setUserInfo(decodedToken);
    }, [])

    return(
        <>
        <h1>Main Page</h1>
        <button onClick={() => {navigate(`/user/${userInfo.given_name}`)}}>My profile</button>
        <button onClick={() => navigate('/search/music')}>Search music</button>
        <LogoutButton/>
        </>
    )
}