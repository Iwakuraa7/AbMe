import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import NavBar from "../components/NavBar";
import styles from "../styles/MainPage.module.css"

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
        <NavBar/>
        <div className={styles["mainPage-back"]}>
            <div className={styles["morphing-shape"]}>
                AbMe
            </div>
            <div className={styles["search-user-input-box"]}>
                <label>Searching for someone?</label>
                <input type="text"/>
            </div>
        </div>
        </>
    )
}