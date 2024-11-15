import { useContext, useEffect, useState } from "react"
import NavBar from "../components/NavBar"
import styles from "../styles/SearchPage.module.css"
import { UserContext } from "../src/contexts/UserContext";

export default function SearchMusic() {
    const {CLIENT_ID, CLIENT_SECRET, UiMsg, setUiMsg} = useContext(UserContext);
    const [searchInput, setSearchInput] = useState('');
    const [albums, setAlbums] = useState(null);
    const [tracks, setTracks] = useState(null);
    const [musicData, setMusicData] = useState(null);

    useEffect(() => {
        var authParams = {
            method: 'POST',
            headers: {
                'Content-type': 'application/x-www-form-urlencoded'
            },
            body: 'grant_type=client_credentials&client_id=' + CLIENT_ID + '&client_secret=' + CLIENT_SECRET
        }
        
        fetch('https://accounts.spotify.com/api/token', authParams)
            .then(result => result.json())
            .then(data => {
                const spotifyToken = data.access_token;
                localStorage.setItem('spotifyToken', spotifyToken);
            });
    },  [])

    async function searchMusic() {
        var searchParams = {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('spotifyToken'),
                'Content-Type': 'application/json'
            }
        }

        // Returns an array of artists, albums, tracks by the given query
        var albums = await fetch('https://api.spotify.com/v1/search?q=' + searchInput + '&type=album,artist,track', searchParams)
            .then(response => response.json())
            .then(data => {console.log(data); setTracks(data.tracks.items); setAlbums(data.albums.items)});

        console.log(albums);
    }

    async function addAlbumData(musicData) {
        var response = await fetch("http://localhost:5078/api/music/create", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Title: musicData.name,
                ArtistName: musicData.artists[0].name,
                ImageUrl: musicData.images[1].url
            })
        })

        var data = await response.json();
        if(data.succeeded) {
            setUiMsg(data.message);
        }
        else {
            console.log(data.message);
        }
    }

    async function addTrackData(musicData) {
        var response = await fetch("http://localhost:5078/api/music/create", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Title: musicData.name,
                ArtistName: musicData.artists[0].name,
                ImageUrl: musicData.album.images[1].url
            })
        })

        var data = await response.json();
        if(data.succeeded) {
            console.log(data.message);
        }
        else {
            console.log(data.message);
        }
    }

    return (
        <>
        <NavBar/>
        <div className={styles["search-main-box"]}>
            <div className={styles["search-input-box"]}>
                <h1>Add music to profile</h1>
                <div className={styles["search-and-button-rel"]}>
                    <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
                    <button onClick={searchMusic}>Search</button>
                </div>
            </div>

            <div className={styles["center-text-box"]}>
                {albums && <h2>Albums</h2>}
            </div>
            <div className={styles["searchBox"]}>
                {albums && albums.map((album, index) => {
                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setMusicData(album)}}
                        onMouseLeave={() => {setMusicData(null)}}
                        >
                            <img src={album.images[1].url}></img><br/>
                            <h3>{album.name}</h3>
                            {musicData === album && (
                                <button onClick={() => addAlbumData(album)}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
            
            <div className={styles["center-text-box"]}>
                {tracks && <h2>Tracks</h2>}
            </div>
            <div className={styles["searchBox"]}>
                {tracks && tracks.map((track, index) => {
                    return(
                        <div
                        key={index}
                        className={styles["searchResultEntity"]}
                        onMouseEnter={() => {setMusicData(track); console.log(track)}}
                        onMouseLeave={() => {setMusicData(null)}}
                        >
                            <img src={track.album.images[1].url}></img><br/>
                            <h3>{track.name}</h3>
                            {musicData === track && (
                                <button onClick={() => {addTrackData(track)}}>Add</button>
                            )}
                        </div>
                    )
                })}
            </div>
        </div>
        </>
    )
}