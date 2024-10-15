import { useNavigate } from "react-router-dom"

export default function NotFoundPage() {
    const navigate = useNavigate();

    const goToHomePage = () => {navigate('/')};

    return(
        <>
        <h1>404 Not Found</h1>
        <button onClick={goToHomePage}>Go back</button>
        </>
    )
}