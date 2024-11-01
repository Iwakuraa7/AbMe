import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import NavBar from "../components/NavBar";
import styles from "../styles/MainPage.module.css"
import searchStyles from "../styles/SearchPage.module.css"

export default function MainPage() {
    const [userInfo, setUserInfo] = useState(null);
    const [userSearchQuery, setUserSearchQuery] = useState(null);
    const [userSearchResult, setUserSearchResult] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        if(localStorage.getItem('token') === null) {
            return navigate('/');
        }

        var decodedToken = jwtDecode(localStorage.getItem('token'));
        setUserInfo(decodedToken);
    }, [])

    async function handleUserSearch() {
        var response = await fetch(`http://localhost:5078/api/account/user-search/${userSearchQuery}`);
        var data = await response.json();

        if(data.succeeded)
            setUserSearchResult(data.usernames);
        else
            console.error("Sth went wrong while finding usernames by the value...")
    }

    return(
        <>
        <NavBar/>
        <div className={styles["mainPage-back"]}>
            <div className={styles["morphing-shape"]}>
                AbMe
            </div>
            <div className={styles["search-user-input-box"]}>
                <label>Searching for someone?</label>
                <div className={searchStyles["search-and-button-rel"]}>
                    <input onChange={(e) => setUserSearchQuery(e.target.value)} type="text"/>
                    <button className={styles["button-style"]} onClick={() => handleUserSearch()}>Find</button>
                </div>
            </div>

            {userSearchResult &&
            (<div className={styles["usernames-box"]}>
                {userSearchResult.map(u => {
                    return (
                        <div className={styles["usernameEntity-box"]} onClick={() => navigate(`/user/${u}`)}>
                            <h3>{u}</h3>
                        </div>
                    )
                })}
            </div>)}
        </div>
        </>
    )
}