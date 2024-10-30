import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./HomePage.module.css"
import SingInPage from "./SignInPage";
import SignUpPage from "./SignUpPage";
import AboutInfo from "../components/AboutInfo";


export default function HomePage() {
    const navigate = useNavigate();
    const [isSignIn, setIsSignIn] = useState(false);
    const [isSignUp, setIsSignUp] = useState(false);
    const [isAbout, setIsAbout] = useState(false);

    useEffect(() => {
        if(localStorage.getItem('token') !== null) {
            navigate('/home');
        }
    });

    return (
        <>
        <div className={styles["main-grid"]}>
            <div className={styles["left-grid-component"]}/>

            {!isSignIn && !isSignUp && (<div className={styles["middle-grid-component"]}>
                <div className={styles["up-middle-grid-component"]}/>

                <div className={styles["down-middle-grid-component"]}/>
            </div>)}

            <div className={styles["right-grid-component"]}>
                <div className={styles["up-right-grid-component"]} onClick={() => {setIsAbout(!isAbout)}}/>

                <div className={styles["middle-right-grid-component"]} onClick={() => {setIsSignIn(!isSignIn); setIsSignUp(false)} }/>

                <div className={styles["down-right-grid-component"]} onClick={() => {setIsSignUp(!isSignUp); setIsSignIn(false)}}/>
            </div>
            
            {isSignIn && (<SingInPage/>)}

            {isSignUp && (<SignUpPage/>)}

            {isAbout && (<AboutInfo/>)}
        </div>
        </>
    )
}