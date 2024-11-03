import { useEffect, useRef, useState } from "react"
import { useParams } from "react-router-dom";
import {jwtDecode } from "jwt-decode";
import styles from "../styles/UserPage.module.css"
import NavBar from "../components/NavBar";

export default function UserPage() {
    const [isOwner, setIsOwner] = useState(null);
    const [musicData, setMusicData] = useState(null);
    const [booksData, setBooksData] = useState(null);
    const [expandedHobby, setExpandedHobby] = useState(null);
    const [dataToDelete, setDataToDelete] = useState(null);
    const params = useParams();
    const hobbyRollBoxRef = useRef(null);

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
                    console.log(data.musicData);
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

    // useEffect(() => {
    //     let scrollAmount = 0;
    //     let speed = 1.5; // Adjust the speed of scrolling

    //     function autoScroll() {
    //         const box = hobbyRollBoxRef.current;

    //         if (box) {
    //             scrollAmount += speed;
    //             if (scrollAmount >= box.scrollWidth - box.clientWidth) {
    //                 scrollAmount = 0; // Reset scroll to loop
    //             }
    //             box.scrollLeft = scrollAmount;
    //         }

    //         requestAnimationFrame(autoScroll); // Smooth looping
    //     }

    //     autoScroll();
    // }, [])

    async function deleteMusicData(musicId) {
        try
        {
            var response = await fetch(`http://localhost:5078/api/music/delete/${musicId}`, {
                method: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    "Content-Type": "application/json"
                }
            })

            var data = await response.json();

            if(data.succeeded)
            {
                setMusicData((prevData) => prevData.filter(m => m.id !== musicId));
                console.log(data.message);
            }
            else
                console.error("sth went wrong during deletion....");
        }
        catch(err)
        {
            console.error(err);
        }
    }

    async function deleteBookData(bookId) {
        try
        {
            var response = await fetch(`http://localhost:5078/api/book/delete/${bookId}`, {
                method: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    "Content-Type": "application/json"
                }
            })

            var data = await response.json();

            if(data.succeeded)
            {
                setBooksData((prevData) => prevData.filter(m => m.id !== bookId));
                console.log(data.message);
            }
            else
                console.error("sth went wrong during deletion....");
        }
        catch(err)
        {
            console.error(err);
        }
    }

    const renderMusicContent = () => {
        return(
        <div>
            <h2>{params.username} music taste</h2>
            <button onClick={() => setExpandedHobby(null)}>Back</button>
            <div className="hobbyRollBox" ref={hobbyRollBoxRef}>
            {musicData.map(music => (
                <div
                key={music.id}
                className="searchResultEntity"
                onMouseEnter={() => setDataToDelete(music.id)}
                onMouseLeave={() => setDataToDelete(null)}>

                <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>
                <h2>{music.title}</h2>
                {isOwner && dataToDelete === music.id && (
                    <button onClick={() => deleteMusicData(dataToDelete)}>Delete</button>
                )}
                </div>
            ))}
            </div>
        </div>)
    }

    const renderBooksContent = () => {
        return(
        <div>
            <h2>{params.username} literature taste</h2>
            <button onClick={() => setExpandedHobby(null)}>Back</button>
            <div className="hobbyRollBox" ref={hobbyRollBoxRef}>
            {booksData.map(book => (
                <div
                key={book.id}
                className="searchResultEntity"
                onMouseEnter={() => setDataToDelete(book.id)}
                onMouseLeave={() => setDataToDelete(null)}
                >

                <img key={book.id} src={book.imageUrl} alt={`Music photo ${book.id + 1}`}/>
                <h2>{book.title}</h2>
                {isOwner && dataToDelete === book.id && (
                    <button onClick={() => deleteBookData(dataToDelete)}>Delete</button>
                )}
                </div>
            ))}
            </div>
        </div>)
    }

    const renderContent = () => {
        switch(expandedHobby) {
            case 'music':
                return renderMusicContent();
            case 'books':
                return renderBooksContent();
            default:
                return null;
        }
    }

    return (
        <>
        {expandedHobby !== null
        ?
        <>
        <NavBar/>
        {renderContent()}
        </>
        :
        (
        <>
        <NavBar/>
        <div>
            <h2>{params.username}</h2>
            <div onClick={() => setExpandedHobby('music')} className="userHobbyBox">
                <div className="userHobbyBoxImages">
                    {musicData && (
                        musicData.slice(0, 4).map(music => (
                            <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className="userHobbyBoxTitle">
                    <strong>Music</strong>
                </div>
            </div>

            <div onClick={() => setExpandedHobby('books')} className="userHobbyBox bookImagesRes">
                <div className="userHobbyBoxImages">
                    {booksData && (
                        booksData.slice(0, 8).map(book => (
                            <img key={book.id} src={book.imageUrl} alt={`Book photo ${book.id + 1}`}/>
                        ))
                    )}
                </div>
                <div className="userHobbyBoxTitle">
                    <strong>Literature</strong>
                </div>
            </div>
        </div>
        </>
        )}
        </>
    )
}