import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import {jwtDecode } from "jwt-decode";

export default function UserPage() {
    const [isOwner, setIsOwner] = useState(null);
    const [musicData, setMusicData] = useState(null);
    const [expandedHobby, setExpandedHobby] = useState(null);
    const [musicToDelete, setMusicToDelete] = useState(null);
    const params = useParams();

    useEffect(() => {
        async function fetchMusicData() {
            try
            {
                var response = await fetch(`http://localhost:5078/api/music/${params.username}`, {
                    method: "GET",
                    headers: {
                        "Authorization": "Bearer " + localStorage.getItem('token'),
                        "Content-Type": "application/json",
                    }
                })

                var data = await response.json();

                if(data.succeeded)
                    setMusicData(data.musicData);

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

        fetchMusicData();
        checkOwnership();
    }, [params.username])

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
                console.errorr("sth went wrong during deletion....");
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
            {musicData.map(music => (
                <div
                key={music.id}
                className="searchResultEntity"
                onMouseEnter={() => setMusicToDelete(music.id)}
                onMouseLeave={() => setMusicToDelete(null)}>

                <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>

                {isOwner && musicToDelete === music.id && (
                    <button onClick={() => deleteMusicData(musicToDelete)}>Delete</button>
                )}
                </div>
            ))}
        </div>)
    }

    return (
        <>
        {expandedHobby !== null
        ?
        (renderMusicContent())
        :
        (
        <div>
            <h2>{params.username}</h2>
            <div onClick={() => setExpandedHobby("music")} className="userHobbyBox">
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
        </div>
        )}
        </>
    )
}