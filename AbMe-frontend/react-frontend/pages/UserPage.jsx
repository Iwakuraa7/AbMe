import { useEffect, useState } from "react"

export default function UserPage() {
    const [musicData, setMusicData] = useState(null);

    useEffect(() => {
        async function fetchMusicData() {
            var response = await fetch("http://localhost:5078/api/music/my", {
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

    return (
        <div>
            <h2>My profile</h2>
            <div className="userHobbyBox">
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
        </div>
    )
}