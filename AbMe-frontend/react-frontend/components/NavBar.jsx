import { useNavigate } from "react-router-dom"
import styles from "../styles/NavBar.module.css"
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../src/contexts/UserContext";
import { jwtDecode } from "jwt-decode";

export default function NavBar() {
    const navigate = useNavigate();
    const { userInfo, setUserInfo } = useContext(UserContext);
    const [isAdd, setIsAdd] = useState(false);

    useEffect(() => {
        var token = localStorage.getItem('token');

        var decodedToken = jwtDecode(token);
        setUserInfo(decodedToken);
    }, [localStorage.getItem('token')])

    return (
        <div className={styles["nav-bar-box"]}>
            <div
            className={`${styles["nav-bar-element"]} ${styles["back-gradient-glYellow-sageGreen"]}`}
            onClick={() => navigate('/home')}
            >
                Home
            </div>

            <div
            className={`${styles["nav-bar-element"]} ${styles["back-gradient-lightPink-red"]} ${styles["add-bar-element"]}`}
            onMouseEnter={() => setIsAdd(true)}
            onMouseLeave={() => setIsAdd(false)}
            >
                Add...
                <div className={styles["dropdown-container"]} style={{ display: isAdd ? 'block' : 'none' }}>
                    <div onClick={() => navigate('/search/music')}>Music</div>
                    <div onClick={() => navigate('/search/book')}>Books</div>
                </div>
            </div> 

            <div
            className={`${styles["nav-bar-element"]} ${styles["back-gradient-blue-hotpink"]}`}
            onClick={() => navigate(`/user/${userInfo.given_name}`)}
            >
                My profile
            </div>

            <div
            className={`${styles["nav-bar-element"]} ${styles["back-gradient-lightBlue-plum"]}`}
            onClick={() => {
                navigate('/');
                localStorage.removeItem('token');
            }}
            >
                Logout
            </div>   
        </div>
    )
}