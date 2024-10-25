import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";

export default function UserPage() {
    const [musicData, setMusicData] = useState(null);
    const [expandedHobby, setExpandedHobby] = useState(null);
    const params = useParams();

    useEffect(() => {
        async function fetchMusicData() {
            var response = await fetch(`http://localhost:5078/api/music/${params.username}`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    "Content-Type": "application/json",
                }
            })

            var data = await response.json();

            if(data.succeeded) {
                setMusicData(data.musicData);
                console.log("+ userMusicData");
            }
            else {
                console.log("sth went wrong while gettin userMusicData...");
            }
        }

        fetchMusicData();
    }, [])

    const renderMusicContent = () => {
        return(
        <div>
            <h2>{params.username} music taste</h2>
            {musicData.map(music => (
                <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>
            ))}
        </div>)
    }


    return (
        <>
        {expandedHobby !== null ? (renderMusicContent()) : (
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
        {/* <div>
            <h2>{params.username}</h2>
            <div onClick={() => setExpandedHobby("music")} className="userHobbyBox">
                <div className="userHobbyBoxImages">
                    {musicData && (
                        musicData.slice(0, 4).map(music => {
                            return(
                                <img key={music.id} src={music.imageUrl} alt={`Music photo ${music.id + 1}`}/>
                            )
                        })
                    )}
                </div>
                <div className="userHobbyBoxTitle">
                    <strong>Music</strong>
                </div>
            </div>
        </div> */}
        </>
    )
}