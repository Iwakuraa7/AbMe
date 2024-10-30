import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./HomePage.module.css"


export default function HomePage() {
    const navigate = useNavigate();

    useEffect(() => {
        if(localStorage.getItem('token') !== null) {
            navigate('/home');
        }
    });

    return (
        <>
        <div className={styles["main-grid"]}>
            <div className={styles["left-grid-component"]}/>

            <div className={styles["middle-grid-component"]}>
                <div className={styles["up-middle-grid-component"]}/>

                <div className={styles["down-middle-grid-component"]}/>
            </div>

            <div className={styles["right-grid-component"]}>
                <div className={styles["up-right-grid-component"]}/>

                <div className={styles["middle-right-grid-component"]} onClick={() => navigate('/signin')}/>

                <div className={styles["down-right-grid-component"]} onClick={() => navigate('/signup')}/>
            </div>
        </div>
        </>
    )
}