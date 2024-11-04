import { useEffect, useRef, useState } from "react"
import { useParams } from "react-router-dom";
import {jwtDecode } from "jwt-decode";
import styles from "../styles/UserPage.module.css"
import mainPageStyles from "../styles/MainPage.module.css"
import NavBar from "../components/NavBar";

export default function UserPage() {
    const [isOwner, setIsOwner] = useState(null);
    const [musicData, setMusicData] = useState(null);
    const [booksData, setBooksData] = useState(null);
    const [animeData, setAnimeData] = useState(null);
    const [mangaData, setMangaData] = useState(null);
    const [randomDimensions, setRandomDimensions] = useState({});
    const [expandedHobby, setExpandedHobby] = useState(null);
    const [dataToDelete, setDataToDelete] = useState(null);
    const params = useParams();

    useEffect(() => {
        async function fetchHobbyData() {
            try
            {
                var response = await fetch(`http://localhost:5078/api/account/user-hobby-data/${params.username}`, {
                    method: "GET",
                    headers: {
                        "Authorization": "Bearer " + localStorage.getItem('token'),
                        "Content-Type": "application/json",
                    }
                })

                var data = await response.json();

                if(data.succeeded) {
                    setMusicData(data.musicData);
                    setBooksData(data.booksData);
                    setAnimeData(data.animeData);
                    setMangaData(data.mangaData);
                    console.log(data);
                }

                else
                    console.error("sth went wrong while gettin userMusicData...");
            }
            catch(err)
            {
                console.error(err);
            }
        }

        function checkOwnership() {
            var token = localStorage.getItem('token');
            var userInfo = jwtDecode(token);

            if(userInfo.given_name === params.username)
                setIsOwner(true);

            else
                setIsOwner(false);
        }

        fetchHobbyData();
        checkOwnership();
    }, [params.username])

    async function deleteRelevantData(dataId) {
        try
        {
            var response = await fetch(`http://localhost:5078/api/${expandedHobby}/delete/${dataId}`, {
                method: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    "Content-Type": "application/json"
                }                
            })

            var data = await response.json();

            if(data.succeeded)
            {
                switch(expandedHobby)
                {
                    case 'music':
                        setMusicData((prevData) => prevData.filter(m => m.id !== dataId));
                    case 'book':
                        setBooksData((prevData) => prevData.filter(b => b.id !== dataId));
                    case 'anime':
                        setAnimeData((prevData) => prevData.filter(a => a.id !== dataId));
                    case 'manga':
                        setMangaData((prevData) => prevData.filter(m => m.id !== dataId));
                    default:
                        return;
                }
                console.log(data.message);
            }
            else
            {
                console.error("sth went wrong during deletion....");
            }
        }
        catch(err)
        {
            console.error(err);
        }
    }

    function findRelevantData() {
        switch(expandedHobby)
        {
            case 'music':
                return musicData;
            case 'book':
                return booksData;
            case 'anime':
                return animeData;
            case 'manga':
                return mangaData;
            default:
                return null;
        }        
    }

    useEffect(() => {
        function createRandomDimensions() {
            const dimensions = {};
            findRelevantData().forEach(data => {
                const randomWidth = expandedHobby === 'book' ? Math.floor(Math.random() * 100 + 100) : Math.floor(Math.random() * 100 + 200);
                const randomHeight = expandedHobby === 'music' ? randomWidth : randomWidth * 1.5;
                dimensions[data.id] = { width: randomWidth, height: randomHeight };
            });
            setRandomDimensions(dimensions);
        }

        if(expandedHobby !== null)
            createRandomDimensions();
    }, [expandedHobby])

    const renderRelevantContent = () => {
        var relevantData = findRelevantData();
    
        return (
            <div>
                <div className={styles["center-upper-elements"]}>
                    <h2>{params.username}'s {expandedHobby} taste</h2>
                    <button className={mainPageStyles["button-style"]} onClick={() => setExpandedHobby(null)}>Back</button>
                </div>

                <div className={styles["random-box"]}>
                    {relevantData.map(data => (
                        <div
                            key={data.id}
                            style={{
                                maxWidth: randomDimensions[data.id]?.width,
                                maxHeight: randomDimensions[data.id]?.height,
                                margin: randomDimensions[data.id]?.width - (expandedHobby === 'book' ? 100 : 200),
                            }}
                            className={styles["expanded-hobby-entity-box"]}
                            onMouseEnter={() => setDataToDelete(data.id)}
                            onMouseLeave={() => setDataToDelete(null)}
                        >
                            <img className={styles["hobby-image"]} src={data.imageUrl} alt={`Data photo ${data.id + 1}`} />
                            <h2 style={{ textAlign: "center", fontSize: "1rem", margin: "5px 0" }}>{data.title}</h2>
                            {isOwner && (
                                <button
                                style={{display: dataToDelete === data.id ? "block" : "none"}}
                                onClick={() => deleteRelevantData(dataToDelete)}
                                className={styles["button-style"]}
                                >
                                    Delete
                                </button>
                            )}
                        </div>
                    ))}
                </div>

            </div>
        );
    };
    
    

    return (
        <>
        {expandedHobby !== null
        ?
        <>
        <NavBar/>
        {renderRelevantContent()}
        </>
        :
        (
        <>
        <NavBar/>
        <h2 className={styles["center-text"]}>{params.username}</h2>
        
        <div className={styles["user-hobby-main-box"]}>

            <div onClick={() => setExpandedHobby('music')} className={styles["userHobbyBox"]}>
                <div className={styles["userHobbyBoxImages"]}>
                    {musicData && (
                        musicData.slice(0, 4).map(music => (
                            <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className={styles["userHobbyBoxTitle"]}>
                    <strong>Music</strong>
                </div>
            </div>

            <div onClick={() => setExpandedHobby('book')} className={`${styles["userHobbyBox"]} ${styles["bookImagesRes"]}`}>
                <div className={styles["userHobbyBoxImages"]}>
                    {booksData && (
                        booksData.slice(0, 8).map(book => (
                            <img key={book.id} src={book.imageUrl} alt={`Book photo ${book.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className={styles["userHobbyBoxTitle"]}>
                    <strong>Literature</strong>
                </div>
            </div>

            <div onClick={() => setExpandedHobby('anime')} className={`${styles["userHobbyBox"]} ${styles["bookImagesRes"]}`}>
                <div className={styles["userHobbyBoxImages"]}>
                    {animeData && (
                        animeData.slice(0, 8).map(anime => (
                            <img key={anime.id} src={anime.imageUrl} alt={`Anime photo ${anime.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className={styles["userHobbyBoxTitle"]}>
                    <strong>Anime</strong>
                </div>
            </div>

            <div onClick={() => setExpandedHobby('manga')} className={`${styles["userHobbyBox"]} ${styles["bookImagesRes"]}`}>
                <div className={styles["userHobbyBoxImages"]}>
                    {mangaData && (
                        mangaData.slice(0, 8).map(manga => (
                            <img key={manga.id} src={manga.imageUrl} alt={`Manga photo ${manga.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className={styles["userHobbyBoxTitle"]}>
                    <strong>Manga</strong>
                </div>
            </div>

        </div>
        </>
        )}
        </>
    )
}