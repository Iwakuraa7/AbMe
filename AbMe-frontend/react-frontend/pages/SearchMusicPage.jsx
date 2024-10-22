import { useEffect, useState } from "react"

const CLIENT_ID = '779748b371464a0b85a3ea0bdcf47f39';
const CLIENT_SECRET = '0233d3de035f42be8d891773b009a1b5';

export default function SearchMusic() {
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
        
        // Make async + error handling
        fetch('https://accounts.spotify.com/api/token', authParams)
            .then(result => result.json())
            .then(data => {
                const spotifyToken = data.access_token;
                // Maybe add if-else statement -> whether to update or not the token
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

        // var artistId = await fetch('https://api.spotify.com/v1/search?q=' + searchInput + '&type=artist', searchParams)
        //     .then(response => response.json())
        //     .then(data => { return data.artists.items[0].id });

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
            console.log(data.message);
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
        <input type='text' onChange={(e) => {setSearchInput(e.target.value)}}></input>
        <button onClick={searchMusic}>Search</button><br/>
        {albums && <h2>Albums</h2>}
        <div className='searchBox'>
            {albums && albums.map((album, index) => {
                return(
                    <div
                    key={index}
                    className="searchResultEntity"
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
        {tracks && <h2>Tracks</h2>}
        <div className='searchBox'>
            {tracks && tracks.map((track, index) => {
                return(
                    <div
                    key={index}
                    className="searchResultEntity"
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
        </>
    )
}