import styles from "../styles/SearchPage.module.css";

export default function ContextMessage(props) {

    return(
        <div className={`${styles["context-msg-box"]} ${props.fadeOut ? styles["fade-out"] : ""}`}>
            {props.message}
        </div>
    )
}